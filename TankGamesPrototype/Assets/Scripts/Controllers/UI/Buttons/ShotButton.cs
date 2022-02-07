using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class ShotButton : Buttons
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            Gun.Shot();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}