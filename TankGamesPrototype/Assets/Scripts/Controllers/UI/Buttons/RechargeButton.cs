using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class RechargeButton : Buttons
    {
        private void Start()
        {
            Gun = GameObject.FindObjectOfType<Bambushka>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            Gun.StartRecharge();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}