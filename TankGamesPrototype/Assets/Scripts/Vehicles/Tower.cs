using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Tower : MonoBehaviour
    {
        Transform gun;
        [SerializeField]
        Transform target;

        CamController cam;

        private void Awake()
        {
            //target = GameObject.Find("TowerTarget").transform;
            gun = FindObjectOfType<Weapons>().transform;
            cam = Camera.main.GetComponent<CamController>();
        }

        public void TowerMovement(float speedTower, float speedGun, float minGunAngle, float maxGunAngle)
        {
            if (target != null)
            {
                Vector3 offsetTower = cam.CamLookPosition(target.position) - this.transform.position;

                offsetTower = Quaternion.Inverse(transform.parent.rotation) * offsetTower;
                offsetTower.y = 0f;
                Quaternion rotTower = Quaternion.LookRotation(offsetTower);
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rotTower, speedTower * Time.deltaTime);

                Vector3 offsetGun = cam.CamLookPosition(target.position) - gun.position;
                offsetGun = Quaternion.Inverse(gun.parent.rotation) * offsetGun;
                offsetGun.y = 0f;
                float angle = Mathf.Atan2((cam.CamLookPosition(target.position) - gun.position).y, offsetGun.magnitude) * Mathf.Rad2Deg;
                if (angle < minGunAngle) angle = minGunAngle;
                if (angle > maxGunAngle) angle = maxGunAngle;
                Quaternion rotGun = Quaternion.LookRotation(offsetGun);
                rotGun = Quaternion.Euler(-angle, 0, 0);
                gun.localRotation = Quaternion.RotateTowards(gun.localRotation, rotGun, speedGun * Time.deltaTime);
            }
            else
            {
                Debug.LogError("target is null", this);
            }
        }
    }
}
