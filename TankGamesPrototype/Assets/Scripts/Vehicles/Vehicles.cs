using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.COLORSGAMES.TANKGAMES
{
    abstract public class Vehicles : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float force;
        public float explosionForce;
        public float angle;
        public float torqueForce;
        public float torqueSpeedMax;
        public float maxHealth;

        public GameObject playerControllPanel;

        public float curretHealth { get; protected set; }
        public float MotorForce { get; set; }
        public float SteerAngle { get; set; }

        public bool Alive { get; protected set; }

        protected Rigidbody RigidB { get; set; }
        protected WheelCollider[] wheels { get; private set; }

        public Transform centerOfMass;
        public AxleInfo[] axleInfos;

        float torqueSpeed;

        protected virtual void Start()
        {
            //tower = GameObject.FindObjectOfType<Tower>();
            RigidB = GetComponent<Rigidbody>();
            centerOfMass = GameObject.Find("CenterOfMass").transform;
            curretHealth = maxHealth;
            playerControllPanel = GameObject.Find("PlayerControllers");
            Alive = true;
            SetCenterOfMass(RigidB, centerOfMass);
            SetWheelsColliders();
        }

        public virtual void Acceleration()
        {
            foreach (AxleInfo item in axleInfos)
            {
                if (item.isMotor && Alive)
                {
                    item.RightCollider.motorTorque = force * MotorForce;
                    item.LeftCollider.motorTorque = force * MotorForce;
                }

                if (item.isSteering && Alive)
                {
                    item.RightCollider.steerAngle = angle * SteerAngle;
                    item.LeftCollider.steerAngle = angle * SteerAngle;
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
                        RigidB.AddTorque(transform.forward * torqueForce * SteerAngle, ForceMode.Acceleration);
                    }
                }
            }
        }

        public void SetWheelsColliders()
        {
            wheels = GameObject.FindObjectsOfType<WheelCollider>();
        }

        public void SetCenterOfMass(Rigidbody rb, Transform target)
        {
            rb.centerOfMass = target.localPosition;
        }

        public void Damage(float damage)
        {
            curretHealth -= damage;
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
                stream.SendNext(curretHealth);
            }
            else
            {
                curretHealth = (float)stream.ReceiveNext();
            }
        }
    }
}