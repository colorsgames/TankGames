using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES {
    public class SpawnDestroyWall : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public GameObject destroyWall;

        PhotonView photonView;

        private void Start()
        {
            photonView = PhotonView.Get(this);
        }

        public void Spawn()
        {
            photonView.RPC("rpcSpawn", RpcTarget.All);
        }

        [PunRPC]
        private void rpcSpawn()
        {
            Instantiate(destroyWall, transform.position, transform.rotation);
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
