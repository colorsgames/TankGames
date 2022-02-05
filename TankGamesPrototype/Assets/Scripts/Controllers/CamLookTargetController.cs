using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class CamLookTargetController : MonoBehaviourPun
    {
        public Vector3 newPosition;

        Transform oldParent;

        private void Start()
        {
            oldParent = transform.parent;
            //transform.parent = null;
        }

        private void Update()
        {
            transform.position = oldParent.position + newPosition;
        }
    }
}