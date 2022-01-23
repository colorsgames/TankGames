using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class ParticleController : MonoBehaviour
    {
        public float lifeTime;

        ParticleSystem particle;

        float curretTime;

        private void Start()
        {
            particle = transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(gameObject, lifeTime + 2);
        }

        private void Update()
        {
            curretTime += Time.fixedDeltaTime;
            if (curretTime > lifeTime)
            {
                var emiss = particle.emission;
                emiss.enabled = false;
            }
        }
    }
}