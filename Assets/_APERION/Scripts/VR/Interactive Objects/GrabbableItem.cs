using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    [RequireComponent(typeof(InteractiveItem))]
    [RequireComponent(typeof(Rigidbody))]
    public class GrabbableItem : MonoBehaviour
    {
        #region EVENTS

        public event Action GrabCallback;
        public event Action UngrabCallback;

        public void OnGrab()
        {
            GrabCallback?.Invoke();          
        }

        public void OnUngrab()
        {
            UngrabCallback?.Invoke();
        }

        #endregion

        #region VARIABLES

        public enum GrabType
        {
            Free
            //LerpToHand
        }

        [Header("Grab Settings")]
        public GrabType grabType;

        public AudioClip grabSFX;

        private InteractiveItem interactiveItem;
        private Rigidbody rb;
        private HandGrabber grabbingHand;
        //private GameObject grabPoint;
        private AudioSource audioSource;
        //private bool startPhysicsGrab;

        #endregion

        private void Awake()
        {
            interactiveItem = GetComponent<InteractiveItem>();
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            interactiveItem.ItemUsedCallback += Grabbed;
            interactiveItem.ItemUnusedCallback += Ungrabbed;
        }

        private void OnDisable()
        {
            interactiveItem.ItemUsedCallback -= Grabbed;
            interactiveItem.ItemUnusedCallback -= Ungrabbed;
        }

        private void Update()
        {
            //transform.position = Vector3.Slerp(transform.position, grabPoint.transform.position, Time.deltaTime * 5F);
            //transform.rotation = Quaternion.Slerp(transform.rotation, grabPoint.transform.rotation, Time.deltaTime * 5F);            
        }

        public bool GetIsGrabbed()
        {
            if (grabbingHand != null)
            {
                return true;
            }

            return false;
        }

        public void SetGrabbedHand(HandGrabber _hand)
        {
            if (grabbingHand != null)
                grabbingHand.SwitchObjectToOtherHand();

            grabbingHand = _hand;
        }

        private void Grabbed()
        {
            transform.parent = grabbingHand.transform;

            rb.isKinematic = true;
            
            rb.useGravity = false;

            audioSource.PlayOneShot(grabSFX);

            OnGrab();
        }

        private void Ungrabbed()
        {            
            transform.parent = null;
                           
            rb.isKinematic = false;

            rb.velocity = grabbingHand.GetControllerPositionVelocity();
            rb.angularVelocity = grabbingHand.GetControllerAngularVelocity();

            rb.useGravity = true;

            grabbingHand = null;

            OnUngrab();
        }      
    }
}

