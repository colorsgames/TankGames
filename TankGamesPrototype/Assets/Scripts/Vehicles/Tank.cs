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
        [SerializeField]
        GameObject teamDetector;

        [SerializeField]
        Color redTeamColor;
        [SerializeField]
        Color blueTeamColor;
        [SerializeField]
        Color noneTeamColor;

        Material teamMat;

        protected override void Start()
        {
            base.Start();
            if (photonView.IsMine)
            {
                meBlueTeam = GameManager.isBlueTeam;
                meRedTeam = GameManager.isRedTeam;
            }
            teamMat = teamDetector.GetComponent<MeshRenderer>().material;
        }

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

            ChangeTeamDetector();
        }

        void ChangeTeamDetector()
        {
            if (meBlueTeam)
            {
                teamMat.color = blueTeamColor;
                teamMat.SetColor("_EmissionColor", blueTeamColor);

            }
            else if (meRedTeam)
            {
                teamMat.color = redTeamColor;
                teamMat.SetColor("_EmissionColor", redTeamColor);
            }
            else
            {
                teamMat.color = noneTeamColor;
                teamMat.SetColor("_EmissionColor", noneTeamColor);
            }
        }
    }
}
