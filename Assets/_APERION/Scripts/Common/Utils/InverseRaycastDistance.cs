using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace APERION.VR
{
    // On 'START', sets an object a certain distance away from the player

    public class InverseRaycastDistance : MonoBehaviour
    {
        [Tooltip("Sets object a certain distance from Vector3.zero")]
        public float distance;

        [Space(7)]
        public bool updatePos;
        public bool faceZero;

        void Start()
        {
            SetDistance(distance);
        }

        void Update()
        {
            if(updatePos)
                SetDistance(distance);

            // Rotates towards Vector3.zero
            if (faceZero)            
                transform.LookAt(Vector3.zero, Vector3.up);           
        }

        // Sets the object a certain distance away from zero
        private void SetDistance(float distance)
        {
            var zeroPos = Vector3.zero;
            var objectPos = transform.position;

            var ray = new Ray(zeroPos, (objectPos - zeroPos));
            transform.position = ray.GetPoint(distance);
        }
    }
}


