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
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(destroyWall.name, transform.position, transform.rotation);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
