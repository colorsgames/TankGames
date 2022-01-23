using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class Chip : MonoBehaviour
    {
        TrailRenderer line;

        SpawnDestroyWall spawnDestroy;

        Rigidbody rigid;

        FixedJoint joint;

        public int _lifeTime = 10;

        public float _radius = 1;
        public float maxSpeed = 5;
        public float breakForce = 1000;
        public float maxTime = 3f;

        public bool haveLive;

        float _speed;
        float _time = 0f;
        float _distance = 0.5f;


        Vector3[] sides = new Vector3[6] { Vector3.up, Vector3.left, Vector3.down, Vector3.right, Vector3.forward, Vector3.back };

        public enum TypeChip : int
        {
            Simple = 0,
            Hard = 1,
            Sphere = 2,
            Soft = 3
        }

        public TypeChip type = TypeChip.Simple;

        private void Awake()
        {
            if (type == TypeChip.Simple || type == TypeChip.Soft) return;
            transform.parent = null;
        }

        private void Start()
        {
            if (GetComponent<TrailRenderer>())
            {
                line = GetComponent<TrailRenderer>();
                line.enabled = false;
            }

            if (GetComponent<SpawnDestroyWall>())
            {
                spawnDestroy = GetComponent<SpawnDestroyWall>();
            }

            if (haveLive)
                Destroy(this.gameObject, _lifeTime);

            if (type == TypeChip.Simple) return;

            if (type != TypeChip.Soft)
            {
                if (!gameObject.GetComponent<Rigidbody>())
                {
                    if (type == TypeChip.Hard)
                    {
                        SeeRaycast();
                    }
                    if (type == TypeChip.Sphere)
                    {
                        SeeShpere();
                    }
                }
                if (transform.parent == null)
                {
                    if (!gameObject.GetComponent<Rigidbody>())
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                }
            }
            else
            {
                if (!gameObject.GetComponent<Rigidbody>())
                {
                    gameObject.AddComponent<Rigidbody>();
                }
                if (gameObject.GetComponent<Rigidbody>())
                {
                    joint = gameObject.AddComponent<FixedJoint>();
                    MadeJoint();
                    if (joint.connectedBody == null)
                    {
                        Destroy(joint);
                    }
                }
            }

            if (GetComponent<Rigidbody>())
            {
                rigid = GetComponent<Rigidbody>();
            }
        }

        private void FixedUpdate()
        {
            if (gameObject.GetComponent<Rigidbody>() & line != null)
            {
                //transform.parent = null;
                _time += Time.fixedDeltaTime;
                if (_time < maxTime)
                {
                    if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > maxSpeed * 3)
                    {
                        line.enabled = true;
                    }
                    else
                        line.enabled = false;
                }
                else
                    line.enabled = false;
            }


            if (transform.parent != null)
            {
                if (transform.parent.GetComponent<Rigidbody>())
                {
                    _speed = transform.parent.GetComponent<Rigidbody>().velocity.magnitude;
                }
                else
                {
                    if (type == TypeChip.Soft)
                        _speed = rigid.velocity.magnitude;
                }
            }

            if (type == TypeChip.Soft) return;

            if (type != TypeChip.Simple)
            {
                if (transform.parent == null)
                {
                    if (!gameObject.GetComponent<Rigidbody>())
                    {
                        gameObject.AddComponent<Rigidbody>();
                    }
                }
            }
        }

        private void Update()
        {
            if (haveLive)
            {
                int fps = (int)(1f / Time.unscaledDeltaTime);

                if (fps < 5)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void SeeShpere()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider item in collider)
            {
                if (item.GetComponent<Chip>())
                {
                    if (!item.GetComponent<Chip>().GetComponent<Rigidbody>())
                    {
                        gameObject.transform.parent = item.GetComponent<Chip>().transform;
                    }
                }
            }
        }

        private void SeeRaycast()
        {
            foreach (Vector3 vec in sides)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(vec), out hit, _distance))
                {
                    if (hit.collider.GetComponent<Chip>())
                    {
                        if (!hit.collider.GetComponent<Chip>().GetComponent<Rigidbody>())
                        {
                            gameObject.transform.parent = hit.collider.GetComponent<Chip>().transform;
                        }
                    }
                }
            }
        }

        private void MadeJoint()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, _radius);

            foreach (Collider item in collider)
            {
                if (item.GetComponent<Chip>())
                {
                    if (item.GetComponent<Chip>().GetComponent<Rigidbody>() != this.GetComponent<Rigidbody>())
                    {
                        joint.connectedBody = item.GetComponent<Chip>().GetComponent<Rigidbody>();
                        joint.breakForce = breakForce;
                        joint.breakTorque = breakForce;
                    }
                }
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (_speed >= maxSpeed)
            {
                // print("SpeedMax");
                if (!other.gameObject.GetComponent<Chip>())
                {
                    if (gameObject.GetComponent<SpawnDestroyWall>())
                    {
                        spawnDestroy.Spawn();
                    }
                    if (!gameObject.GetComponent<Rigidbody>())
                    {
                        // print("AddRB");
                        gameObject.AddComponent<Rigidbody>();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (type == TypeChip.Hard)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * _distance);
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _distance);
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _distance);
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * _distance);
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * _distance);
                Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * _distance);
            }
            if (type == TypeChip.Sphere)
            {
                Gizmos.DrawWireSphere(transform.position, _radius);
            }
        }
    }
}
