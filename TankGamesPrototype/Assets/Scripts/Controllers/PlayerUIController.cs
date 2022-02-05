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
        private Vector3 newPosition;

        Camera cam;
        Canvas canvas;


        private void Start()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            transform.parent = canvas.transform;

            cam = Camera.main;

            if (photonView.IsMine)
            {
                Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            transform.position = (Vector2)cam.WorldToScreenPoint(player.transform.position + newPosition);

            playerNameText.text = player.photonView.Owner.NickName;
            healthBar.fillAmount = player.curretHealth / player.maxHealth;
        }
    }
}
