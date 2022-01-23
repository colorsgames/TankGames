using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Explosion : MonoBehaviour
    {
        [Header("Set in Inspector")]
        public float radius;
        public float force;
        public float lifeTime;
        public int damage = 200;
        public LayerMask layerMask;

        private SphereCollider explColl;

        private void Start()
        {
            Destroy(this.gameObject, lifeTime);
            explColl = gameObject.AddComponent<SphereCollider>();
            explColl.radius = radius;
            explColl.isTrigger = true;
        }

        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask, QueryTriggerInteraction.Ignore);
            foreach (Collider item in hitColliders)
            {

                if (item.GetComponent<SpawnDestroyWall>())
                {
                    item.GetComponent<SpawnDestroyWall>().Spawn();
                }
                if (item.GetComponent<Chip>())
                {
                    if (!item.GetComponent<Chip>().GetComponent<Rigidbody>())
                    {
                        item.GetComponent<Chip>().gameObject.AddComponent<Rigidbody>();
                    }
                }
                if (item.GetComponent<Rigidbody>())
                {
                    item.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
                }

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Vehicles>())
            {
                other.GetComponent<Vehicles>().Damage(damage);
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
