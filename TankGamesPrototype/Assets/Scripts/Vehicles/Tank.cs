using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Tank : Vehicles
    {
        public float speedTower, speedGun, maxAngle, minAngle;

        [SerializeField]
        Tower tower;
        CamController camController;

        private void Start()
        {
            //tower = GameObject.FindObjectOfType<Tower>();
            RigidB = GetComponent<Rigidbody>();
            centerOfMass = GameObject.Find("CenterOfMass").transform;
            curretHealth = maxHealth;
            camController = Camera.main.GetComponent<CamController>();
            playerControllPanel = GameObject.Find("PlayerControllers");
            Alive = true;
            if (camController != null)
            {
                if (photonView.IsMine)
                {
                    camController.StartFollowing();
                }
            }
            else
            {
                Debug.LogError("camController is null", this);
            }
            SetCenterOfMass(RigidB, centerOfMass);
            SetWheelsColliders();
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Alive)
                {
                    Coup();
                    if (tower != null)
                    {
                        tower.TowerMovement(speedTower, speedGun, minAngle, maxAngle);
                    }
                }
                else
                {
                    MotorForce = 0;
                }
                Acceleration();

                if (curretHealth <= 0 && Alive)
                {
                    Destroy();
                }
            }
        }

    }
}
