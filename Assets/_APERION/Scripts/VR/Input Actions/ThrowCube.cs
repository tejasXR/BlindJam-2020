using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INPUTACTIONS
{
    public class ThrowCube : InputAction
    {
        public GameObject cubePrefab;

        public float throwForce;

        public void Throw()
        {
            var cube = Instantiate(cubePrefab, transform.position, Quaternion.identity) as GameObject;

            cube.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}


