using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public abstract class Weapons : MonoBehaviour
    {
        public int allAmmo;
        public int maxAmmo;

        public float force;
        public float rechargeTime;

        public PhotonView photonView;

        public int curretAmmo { get; protected set; }
        protected bool recharging;

        public virtual void Shot() { }

        protected void Recharge()
        {
            if (allAmmo > 0 && curretAmmo != maxAmmo)
            {
                int needAmmo = maxAmmo - curretAmmo;
                if (allAmmo < needAmmo)
                {
                    curretAmmo += allAmmo;
                    allAmmo = 0;
                }
                else
                {
                    allAmmo -= needAmmo;
                    curretAmmo += needAmmo;
                }
            }
        }

        public void StartRecharge()
        {
            if (allAmmo > 0 && curretAmmo != maxAmmo)
            {
                recharging = true;
            }
        }

    }
}
