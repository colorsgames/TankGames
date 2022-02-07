using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class BrakeButton : Buttons
    {
        public override void Start()
        {
            player = FindObjectOfType<Vehicles>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {

        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            player.BrakedInput = 1;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            player.BrakedInput = 0;
        }
    }
}