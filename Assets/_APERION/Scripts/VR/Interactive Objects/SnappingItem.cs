using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace APERION.VR.INTERACTIVE
{
    [RequireComponent(typeof(GrabbableItem), typeof(Rigidbody))]    
    public class SnappingItem : MonoBehaviour
    {
        #region EVENTS

        public event UnityAction SnappedCallback;

        protected void OnSnapped()
        {
            if (SnappedCallback != null)
            {
                SnappedCallback?.Invoke();
            }
        }

        #endregion

        public string objectName;

        public UnityEvent snappingAction;

        //private InteractiveItem interactiveItem;
        private Rigidbody rb;
        private GrabbableItem grabbableItem;
        private SnappingSpace snappingSpace;

        private void Awake()
        {
            //interactiveItem = GetComponent<InteractiveItem>();
            rb = GetComponent<Rigidbody>();
            grabbableItem = GetComponent<GrabbableItem>();
        }

        private void OnEnable()
        {
            grabbableItem.UngrabCallback += CheckSnappingCollision;
        }

        private void OnDisable()
        {
            grabbableItem.UngrabCallback -= CheckSnappingCollision;
        }

        public void AssignSnappingCollider(SnappingSpace _snappingCollider)
        {
            snappingSpace = _snappingCollider;
        }

        private void CheckSnappingCollision()
        {
            if (snappingSpace != null)
            {
                Snap();
            }
        }

        private void Snap()
        {
            snappingSpace.ObjectSnapped();

            rb.isKinematic = true;
            rb.useGravity = false;

            transform.position = snappingSpace.transform.position;
            transform.rotation = snappingSpace.transform.rotation;

            if (snappingAction != null)
                snappingAction.Invoke();

            OnSnapped();
        }        
    }
}

