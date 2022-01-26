using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Shell : MonoBehaviour
    {
        public LayerMask layerMask;

        public GameObject explosionPrefab;
        public GameObject effectPrefab;
        public float distance;
        public int lifeTime;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask, QueryTriggerInteraction.Ignore))
            {
                Dead();
            }
        }

        void Dead()
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(explosionPrefab.name, transform.position, Quaternion.identity);
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance);
        }
    }
}