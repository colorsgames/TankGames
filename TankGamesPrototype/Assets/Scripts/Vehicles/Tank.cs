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

        protected override void Start()
        {
            base.Start();
            if (photonView.IsMine)
            {
                meBlueTeam = GameManager.isBlueTeam;
                meRedTeam = GameManager.isRedTeam;
            }
            Invoke("OnPhotonSerializeView", 0);
            Material teamRend = teamDetector.GetComponent<MeshRenderer>().material;
            if (meBlueTeam)
            {
                teamRend.color = blueTeamColor;
                teamRend.SetColor("_EmissionColor", blueTeamColor);

            }
            else if (meRedTeam)
            {
                teamRend.color = redTeamColor;
                teamRend.SetColor("_EmissionColor", redTeamColor);
            }
            else
            {
                teamRend.color = noneTeamColor;
                teamRend.SetColor("_EmissionColor", noneTeamColor);
            }
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
        }

    }
}
