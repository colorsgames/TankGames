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
        private GameObject child;
        [SerializeField]
        private Vector3 screenOffset;
        [SerializeField]
        private float scaleFactor = 10;
        [SerializeField]
        private float visibilityDistance;

        Vector3 targetPos;
        Vector3 oldScale;

        Camera cam;
        Canvas canvas;

        Collider playerCollider;

        private void Start()
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            transform.parent = canvas.transform;
            oldScale = transform.localScale;
            cam = Camera.main;
            playerCollider = player.GetComponent<Collider>();

            if (photonView.IsMine)
            {
                Destroy(gameObject);
            }
        }

        private void LateUpdate()
        {
            targetPos = player.transform.position + screenOffset;
            transform.position = cam.WorldToScreenPoint(targetPos);

            Vector3 offsetToTarget = targetPos - cam.transform.position;
            float distance = offsetToTarget.magnitude;

            transform.localScale = (oldScale / distance) * scaleFactor;

            if (CamController.checkVisable(cam, playerCollider) && distance <= visibilityDistance)
            {
                child.SetActive(true);
            }
            else
                child.SetActive(false);

            playerNameText.text = player.photonView.Owner.NickName;
            healthBar.fillAmount = player.CurretHealth / player.maxHealth;
        }
    }
}
