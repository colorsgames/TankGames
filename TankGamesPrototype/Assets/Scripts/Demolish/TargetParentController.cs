using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class TargetParentController : MonoBehaviour
    {
        private void Update()
        {
            if (transform.childCount == 0)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
        }
    }
}
