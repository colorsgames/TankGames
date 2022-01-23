using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.COLORSGAMES.TANKGAMES
{
    public abstract class Buttons : MonoBehaviour, IPointerClickHandler
    {
        protected Weapons Gun { get; set; }
        public abstract void OnPointerClick(PointerEventData eventData);
    }
}