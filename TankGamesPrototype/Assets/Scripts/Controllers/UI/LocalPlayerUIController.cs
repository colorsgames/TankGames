using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class LocalPlayerUIController : MonoBehaviour
    {
        TMP_Text ammoText;

        Weapons weapons;

        Image healthBar;

        Vehicles vehicle;

        GameObject playerUIControl;

        private void Start()
        {
            ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
            weapons = FindObjectOfType<Weapons>();
            healthBar = GameObject.Find("Health").GetComponent<Image>();
            vehicle = FindObjectOfType<Vehicles>();
            playerUIControl = GameObject.Find("PlayerControllers");
            if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                playerUIControl.SetActive(false);
            }
        }

        private void Update()
        {
            ammoText.text = "Ammo: " + weapons.curretAmmo + " / " + weapons.allAmmo;
            healthBar.fillAmount = vehicle.CurretHealth / vehicle.maxHealth;
        }
    }
}