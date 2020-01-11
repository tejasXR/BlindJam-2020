using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    // On 'START' Makes an object look at another object
    public class LookAt : MonoBehaviour
    {
        public Transform targetTransform;

        public bool xAxis;
        public bool yAxis;
        public bool zAxis;

        private void Start()
        {
            if (targetTransform != null)
                LookAtTarget(targetTransform);
        }

        private void Update()
        {
            Vector3 direction = (targetTransform.position - transform.position);

            // Use for debugging
                // Debug.DrawRay(transform.position, transform.right, Color.green);
                // Debug.DrawRay(transform.position, direction, Color.blue);
        }

        private void LookAtTarget(Transform point)
        {
            Vector3 direction = (point.position - transform.position);
            transform.right = direction;
            transform.eulerAngles = new Vector3(0F, transform.eulerAngles.y, 0F);
        }
    }

}

