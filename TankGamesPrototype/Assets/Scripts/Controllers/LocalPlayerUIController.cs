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

        private void Start()
        {
            ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
            weapons = GameObject.FindObjectOfType<Weapons>();
            healthBar = GameObject.Find("Health").GetComponent<Image>();
            vehicle = GameObject.FindObjectOfType<Vehicles>();
        }

        private void Update()
        {
            ammoText.text = "Ammo: " + weapons.curretAmmo + " / " + weapons.allAmmo;
            healthBar.fillAmount = vehicle.curretHealth / vehicle.maxHealth;
        }
    }
}