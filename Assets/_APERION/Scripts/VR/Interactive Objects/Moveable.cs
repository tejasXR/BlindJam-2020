using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    public class Moveable : MonoBehaviour
    {
        private Rigidbody rb;
        private InteractiveItem interactiveItem;
        private Transform attachPoint;

        private bool isStatic;
        //private bool isStatic;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            interactiveItem = GetComponent<InteractiveItem>();
        }

        private void OnEnable()
        {
            interactiveItem.ItemUsedCallback += OnObjectUsed;
            interactiveItem.ItemUnusedCallback += OnObjectUnused;
        }
        private void OnDisable()
        {
            interactiveItem.ItemUsedCallback -= OnObjectUsed;
            interactiveItem.ItemUnusedCallback -= OnObjectUnused;
        }

        private void Update()
        {
            if (attachPoint != null)
            {
                FollowAttachPoint();
            }

            if (transform.localScale.x < 1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 3F);
            }
        }

        public void MakeStatic()
        {
            isStatic = true;

            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public void SetAttachPoint(Transform _attatch)
        {
            attachPoint = _attatch;
        }

        private void OnObjectUsed()
        {
            if (!isStatic)
            {
                rb.useGravity = false;
                rb.isKinematic = true;

                // Commenting out for now

                //SetAttachPoint(PlayerManager.Instance.rightController.GetComponent<Hand>().grabPoint);                
            }        
        }

        private void OnObjectUnused()
        {
            if (!isStatic)
            {
                rb.useGravity = true;
                rb.isKinematic = false;

                // Commenting out for now

                //rb.velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
                //rb.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);

                SetAttachPoint(null);                
            }           
        }
       
        private void FollowAttachPoint()
        {
            transform.position = Vector3.Lerp(transform.position, attachPoint.position, Time.deltaTime * 5F);
            transform.rotation = Quaternion.Slerp(transform.rotation, attachPoint.rotation, Time.deltaTime * 5F);
        }        
    }
}