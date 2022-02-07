using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.COLORSGAMES.TANKGAMES
{
    abstract public class Vehicles : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float motorForce;
        public float explosionForce;
        public float angle;
        public float brakeForce;
        public float torqueForce;
        public float torqueSpeedMax;
        public float maxHealth;

        public GameObject playerControllPanel;

        public float CurretHealth { get; protected set; }
        public float MotorInput { get; set; }
        public float SteerAngleInput { get; set; }
        public float BrakedInput { get; set; }

        public bool Alive { get; protected set; }

        protected Rigidbody RigidB { get; set; }
        protected WheelCollider[] wheels { get; private set; }
        protected float CurretBrakeForce { get;set; }

        public Transform centerOfMass;
        public AxleInfo[] axleInfos;

        float torqueSpeed;

        protected virtual void Start()
        {
            //tower = GameObject.FindObjectOfType<Tower>();
            RigidB = GetComponent<Rigidbody>();
            centerOfMass = GameObject.Find("CenterOfMass").transform;
            CurretHealth = maxHealth;
            playerControllPanel = GameObject.Find("PlayerControllers");
            Alive = true;
            SetCenterOfMass(RigidB, centerOfMass);
            CurretBrakeForce = brakeForce;
        }

        public virtual void Acceleration()
        {
            foreach (AxleInfo item in axleInfos)
            {
                if (item.isMotor && Alive)
                {
                    if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        MotorInput = Input.GetAxis("Vertical");
                    }
                    item.RightCollider.motorTorque = motorForce * MotorInput;
                    item.LeftCollider.motorTorque = motorForce * MotorInput;
                }

                if (item.isSteering && Alive)
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        SteerAngleInput = Input.GetAxis("Horizontal");
                    }
                    item.RightCollider.steerAngle = angle * SteerAngleInput;
                    item.LeftCollider.steerAngle = angle * SteerAngleInput;
                }

                if(item.isBrake && Alive)
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                    {
                        BrakedInput = Input.GetAxis("Jump");
                    }

                    if (item.isSteering)
                    {
                        brakeForce = CurretBrakeForce * 4;
                    }
                    else
                        brakeForce = CurretBrakeForce;

                    item.RightCollider.brakeTorque = brakeForce * BrakedInput;
                    item.LeftCollider.brakeTorque = brakeForce * BrakedInput;
                }

                VisWheelToCollider(item.RightVisWheel, item.RightCollider);
                VisWheelToCollider(item.LeftVisWheel, item.LeftCollider);
            }
        }

        private void VisWheelToCollider(Transform Vis, WheelCollider Coll)
        {
            Vector3 pos;
            Quaternion rot;

            Coll.GetWorldPose(out pos, out rot);

            Vis.position = pos;
            Vis.rotation = rot;
        }

        public void Coup()
        {
            foreach (WheelCollider item in wheels)
            {
                WheelHit hit;
                if (!item.GetGroundHit(out hit))
                {
                    torqueSpeed = Mathf.Abs(RigidB.angularVelocity.z);
                    if (torqueSpeed < torqueSpeedMax)
                    {
                        RigidB.AddTorque(transform.forward * torqueForce * SteerAngleInput, ForceMode.Acceleration);
                    }
                }
            }
        }

        public void SetWheelsColliders()
        {
            if(photonView.IsMine)
                wheels = GameObject.FindObjectsOfType<WheelCollider>();
        }

        public void SetCenterOfMass(Rigidbody rb, Transform target)
        {
            rb.centerOfMass = target.localPosition;
        }

        public void Damage(float damage)
        {
            CurretHealth -= damage;
        }

        public void Destroy()
        {
            Alive = false;
            RigidB.AddForce(Vector3.up * explosionForce, ForceMode.Acceleration);
            playerControllPanel.SetActive(false);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(CurretHealth);
            }
            else
            {
                CurretHealth = (float)stream.ReceiveNext();
            }
        }
    }
}