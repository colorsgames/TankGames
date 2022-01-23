using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class DebugTextController : MonoBehaviour
    {
        [SerializeField]
        private float lifeTime = 8f;

        float curretTime;

        TMP_Text text;

        private void Start()
        {
            text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (text.text.Length > 0)
            {
                curretTime += Time.deltaTime;
                if (curretTime > lifeTime)
                {
                    text.text = string.Empty;
                    curretTime = 0;
                }
            }
        }

        public void Log(string mess)
        {
            Debug.Log(mess);
            text.text += "\n";
            text.text += mess;
        }
    }
}