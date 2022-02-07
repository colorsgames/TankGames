using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.COLORSGAMES.TANKGAMES
{
    public abstract class Buttons : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        protected Weapons Gun { get; set; }
        protected Vehicles player { get; set; }

        public virtual void Start()
        {
            Gun = FindObjectOfType<Bambushka>();
        }

        public abstract void OnPointerClick(PointerEventData eventData);
        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnPointerUp(PointerEventData eventData);
    }
}