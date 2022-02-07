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
            Vehicles.playerEvent.AddListener(OnPlayerSpawned);
        }

        void OnPlayerSpawned()
        {
            weapons = FindObjectOfType<Weapons>();
            vehicle = FindObjectOfType<Vehicles>();
            ammoText = GameObject.Find("AmmoText").GetComponent<TMP_Text>();
            healthBar = GameObject.Find("Health").GetComponent<Image>();
        }

        private void Update()
        {
            if (weapons != null && vehicle != null)
            {
                ammoText.text = "Ammo: " + weapons.curretAmmo + " / " + weapons.allAmmo;
                healthBar.fillAmount = vehicle.CurretHealth / vehicle.maxHealth;
            }
        }
    }
}