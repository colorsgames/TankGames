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

        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Alive)
                {
                    if (tower != null)
                    {
                        tower.TowerMovement(speedTower, speedGun, minAngle, maxAngle);
                    }
                    Coup();
                }
                else
                {
                    MotorInput = 0;
                }
                Acceleration();

                if (CurretHealth <= 0 && Alive)
                {
                    Destroy();
                }
            }
        }

    }
}
