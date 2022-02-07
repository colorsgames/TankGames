using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Bambushka : Weapons
    {
        public Transform targetInstance;
        public GameObject shell;

        float curretTime;

        private void Start()
        {
            curretAmmo = maxAmmo;
        }

        private void Update()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shot();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartRecharge();
                }
            }
            if (recharging)
            {
                curretTime += Time.deltaTime;
                if (curretTime > rechargeTime)
                {
                    Recharge();
                    curretTime = 0;
                    recharging = false;
                }
            }
        }

        public override void Shot()
        {
            if (curretAmmo > 0 & !recharging)
            {
                curretAmmo--;

                Rigidbody shellRb = PhotonNetwork.Instantiate(shell.name, targetInstance.position, targetInstance.rotation).GetComponent<Rigidbody>();
                //Rigidbody shellRb = Instantiate<Rigidbody>(shell.GetComponent<Rigidbody>(), targetInstance.position, targetInstance.rotation);
                shellRb.AddForce(targetInstance.forward * force, ForceMode.Acceleration);
            }
        }
    }
}
