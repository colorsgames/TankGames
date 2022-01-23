using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class CamControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public CamController camController;

        Vector2 touchDist;

        int pointedIndex;

        bool pressed;

        Vector2 pointerOld;

        public float Horizontal => touchDist.x;
        public float Vertical => touchDist.y;


        void Start()
        {
            camController = Camera.main.GetComponent<CamController>();
        }

        void LateUpdate()
        {
            if (pressed)
            {
                if (pointedIndex >= 0 && pointedIndex < Input.touches.Length)
                {
                    touchDist = Input.touches[pointedIndex].position - pointerOld;
                    pointerOld = Input.touches[pointedIndex].position;
                }
                else
                {
                    touchDist = (Vector2)Input.mousePosition - pointerOld;
                    pointerOld = Input.mousePosition;
                }
            }
            else
            {
                touchDist = Vector2.zero;
            }

            camController.SetInputs(Vertical * camController.sensitivity * Time.fixedDeltaTime, Horizontal * camController.sensitivity * Time.fixedDeltaTime);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pressed = true;
            pointedIndex = eventData.pointerId;
            pointerOld = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pressed = false;
        }

    }
}