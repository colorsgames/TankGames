using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.COLORSGAMES.TANKGAMES
{
    public class CamController : MonoBehaviour
    {
        public LayerMask layer, obstacles;

        public Vector2 rotInput;

        public float maxDistance;
        public float maxAngle, minAngle;
        public float sensitivity;
        public float lerpSpeed;
        public float whaitTime = 2;

        float rotY, rotX;
        float curretTime;
        float oldRotX;

        bool isFollowing;

        Vector3 offset;
        Vector3 targetTowerOffset;

        Vector2 oldMousePos;
        Vector2 mouseOffset;

        Transform lookTarget;
        Transform targetTower;

        Camera cam;

        private void Start()
        {
             Vehicles.playerEvent.AddListener(StartFollowing);
        }

        private void LateUpdate()
        {
            if (!isFollowing) return;

            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                rotInput.x += Input.GetAxis("Mouse Y") * sensitivity * 5 * Time.fixedDeltaTime;
                rotInput.y += Input.GetAxis("Mouse X") * sensitivity * 5 * Time.fixedDeltaTime;
            }

            rotInput.x = Mathf.Clamp(rotInput.x, minAngle, maxAngle);

            rotX = rotInput.x;
            rotY = rotInput.y;

            rotX = Mathf.Clamp(rotX, minAngle, maxAngle);

            if (!GameManager.isBlueTeam)
            {
                rotX *= -1;
            }

            Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);

            transform.position = lookTarget.position - (rotation * offset);
            targetTower.position = lookTarget.position - (rotation * targetTowerOffset);

            Vector3 dir = transform.position - lookTarget.position;
            float dist = Vector3.Distance(lookTarget.position, transform.position - transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(lookTarget.position, dir, out hit, dist, obstacles))
            {
                if (Vector3.Distance(lookTarget.position, hit.point) > 3)
                {
                    transform.position = hit.point + transform.forward;
                    transform.LookAt(lookTarget);
                }
            }
            transform.LookAt(lookTarget);
        }

        public void SetInputs(float x, float y)
        {
            rotInput.x += x;
            rotInput.y += y;
        }

        public Vector3 CamLookPosition(Vector3 target)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, layer, QueryTriggerInteraction.Ignore))
            {
                return hit.point;
            }
            else
            {
                return target;
            }
        }

        void StartFollowing()
        {
            if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            targetTower = FindObjectOfType<TowerTarget>().transform;
            lookTarget = FindObjectOfType<CamLookTargetController>().transform;

            //targetTower.parent = null;

            rotX = transform.eulerAngles.x;
            rotY = transform.eulerAngles.y;

            float camZoffset = 10;

            if (!GameManager.isBlueTeam)
            {
                camZoffset *= -1;
            }

            transform.position = lookTarget.position + new Vector3(0, 0, camZoffset);

            oldRotX = rotX;
            offset = lookTarget.position - transform.position;
            targetTowerOffset = lookTarget.position - targetTower.position;
            cam = Camera.main;

            //Joystick _joystick = GameObject.FindObjectOfType<Joystick>();
            //_joystick.player = GameObject.FindObjectOfType<Vehicles>();

            isFollowing = true;
        }

        static public bool checkVisable(Camera _cam, Collider collider)
        {
            return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(_cam), collider.bounds);
        }
    }
}