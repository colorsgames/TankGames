using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider RightCollider;
        public WheelCollider LeftCollider;

        public Transform RightVisWheel;
        public Transform LeftVisWheel;

        public bool isMotor;
        public bool isSteering;
    }
}
