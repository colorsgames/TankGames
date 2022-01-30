using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES {
    public class SpawnDestroyWall : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public GameObject destroyWall;

        public void Spawn()
        {
            Instantiate(destroyWall, transform.position, transform.rotation);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
