using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INPUTACTIONS
{
    public class ThrowSphere : MonoBehaviour
    {
        public GameObject spherePrefab;

        public float throwForce;

        public void Throw()
        {
            var sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity) as GameObject;

            sphere.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}


