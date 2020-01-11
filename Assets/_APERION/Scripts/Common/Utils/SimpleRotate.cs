using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public class SimpleRotate : MonoBehaviour
    {
        public float rotateSpeed;

        public bool rotateAxisX;
        public bool rotateAxisY;
        public bool rotateAxisZ;

        void Update()
        {
            if (rotateAxisX)
                transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);

            if (rotateAxisY)
                transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);

            if (rotateAxisZ)
                transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
        }
    }
}


