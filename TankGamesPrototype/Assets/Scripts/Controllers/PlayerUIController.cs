using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class PlayerUIController : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private TMP_Text playerNameText;
        [SerializeField]
        private Image healthBar;
        [SerializeField]
        private Vehicles player;
        [SerializeField]
        private Vector3 screenOffset;
        [SerializeField]
        private float scaleFactor = 10;

        Vector3 targetPos;
        Vector3 oldScale;

        Camera cam;
        Canvas canvas;


        private void Start()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            transform.parent = canvas.transform;
            oldScale = transform.localScale;
            cam = Camera.main;

            if (photonView.IsMine)
            {
                Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            targetPos = player.transform.position + screenOffset;
            transform.position = (Vector2)cam.WorldToScreenPoint(targetPos);

            Vector3 offsetToTarget = targetPos - cam.transform.position;

            transform.localScale = (oldScale / offsetToTarget.magnitude) * scaleFactor;

            playerNameText.text = player.photonView.Owner.NickName;
            healthBar.fillAmount = player.curretHealth / player.maxHealth;
        }
    }
}
