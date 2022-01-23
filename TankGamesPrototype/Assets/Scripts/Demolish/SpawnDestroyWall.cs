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
            GameObject chips = PhotonNetwork.Instantiate(destroyWall.name, transform.position, transform.rotation) as GameObject;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
