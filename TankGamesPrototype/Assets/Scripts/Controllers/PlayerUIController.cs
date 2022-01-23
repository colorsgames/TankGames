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

        Transform oldParent;

        Canvas canvas;

        Camera cam;

        private void Start()
        {
            canvas = GetComponent<Canvas>();
            cam = Camera.main;
            canvas.worldCamera = cam;
            oldParent = transform.parent;

            if (photonView.IsMine)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                this.gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            transform.position = oldParent.position + newPosition;

            playerNameText.text = player.photonView.Owner.NickName;
            healthBar.fillAmount = player.curretHealth / player.maxHealth;
            transform.LookAt(cam.transform.position);
        }
    }
}
