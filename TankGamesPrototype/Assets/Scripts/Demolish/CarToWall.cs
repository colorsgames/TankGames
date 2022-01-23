using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class CarToWall : MonoBehaviour
    {
        public int maxSpeedWall;

        private int speed;

        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }


        void FixedUpdate()
        {
            speed = System.Convert.ToInt32(_rigidbody.velocity.magnitude);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (speed > maxSpeedWall)
            {
                if (other.gameObject.GetComponent<SpawnDestroyWall>())
                {
                    other.gameObject.GetComponent<SpawnDestroyWall>().Spawn();
                }

                if (other.gameObject.GetComponent<Chip>())
                {
                    if (!other.gameObject.GetComponent<Chip>().GetComponent<Rigidbody>())
                        other.gameObject.GetComponent<Chip>().gameObject.AddComponent<Rigidbody>();
                }
            }
        }
    }
}
