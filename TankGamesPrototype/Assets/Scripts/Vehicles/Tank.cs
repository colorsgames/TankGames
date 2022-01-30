using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Tank : Vehicles
    {
        public float speedTower, speedGun, maxAngle, minAngle;

        [SerializeField]
        Tower tower;
        CamController camController;

        protected override void Start()
        {
            base.Start();
            camController = Camera.main.GetComponent<CamController>();
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
