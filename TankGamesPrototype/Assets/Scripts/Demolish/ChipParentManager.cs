using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipParentManager : MonoBehaviour
{
    public GameObject targetParent;

    private GameObject[] chips;

    private void Awake()
    {
        chips = GameObject.FindGameObjectsWithTag("WallParticle");
        print(chips.Length);
    }

    private void FixedUpdate()
    {
        //chips = GameObject.FindGameObjectsWithTag("WallParticle");

        foreach (GameObject item in chips)
        {
            if (item.GetComponent<Rigidbody>() && item.gameObject.transform.childCount > 0)
            {
                GameObject _parent = Instantiate<GameObject>(targetParent, item.transform.position, Quaternion.identity);

                item.GetComponent<Rigidbody>().transform.parent = _parent.transform;

                //var _rb = item.GetComponent<Rigidbody>();

                //Destroy(_rb);

                _parent.AddComponent<Rigidbody>();
                _parent.GetComponent<Rigidbody>().mass = 50;
            }
        }
    }
}
