using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    [RequireComponent(typeof(InteractiveItem))]
    public class PushButton : MonoBehaviour
    {
        public float xThreshold;
        public float resistanceForce;

        private Rigidbody rb;
        private InteractiveItem interactiveItem;

        private bool isPushable;
        private Vector3 originLocalPos;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            interactiveItem = GetComponent<InteractiveItem>();
        }

        void Start()
        {
            originLocalPos = transform.localPosition;
            isPushable = true;
        }

        // Simplifying push button for now
        private void OnCollisionEnter(Collision collision)
        {
            interactiveItem.OnItemUsed();
        }

        /*
        private void FixedUpdate()
        {
            var localPos = transform.localPosition;

            if (localPos.x > xThreshold)
            {
                localPos.x = xThreshold;
                transform.localPosition = localPos;

                if (isPushable)
                    OnPressed();               
            }

            if (Mathf.Abs(localPos.x - originLocalPos.x) > .001F && isPushable)
            {
                rb.AddForce((originLocalPos - localPos) * resistanceForce);
                //Debug.Log("Adding force");                
            }

            //if (localPos.x > originLocalPos.x)
            //{
            //    localPos.x = originLocalPos.x;
            //    transform.localPosition = localPos;
            //}

            if (!isPushable)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, originLocalPos, Time.deltaTime * .75F);
            }

            LockYZValues();
        }

        private void OnPressed()
        {
            StartCoroutine(Pressed());
        }

        private IEnumerator Pressed()
        {
            isPushable = false;

            interactiveItem.OnItemUsed();

            rb.isKinematic = true;

            yield return new WaitForSeconds(2F);

            rb.isKinematic = false;

            isPushable = true;
        }

        private void LockYZValues()
        {
            var localPos = transform.localPosition;

            localPos.y = 0;
            localPos.z = 0;

            transform.localPosition = localPos;
        }
        */
    }
}