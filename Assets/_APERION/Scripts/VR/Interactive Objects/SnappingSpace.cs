using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    [RequireComponent(typeof(InteractiveItem))]
    [RequireComponent(typeof(Collider))]
    public class SnappingSpace : MonoBehaviour
    {
        #region VARIABLES

        public List<string> objectsToSnap = new List<string>();
        public Material ghostMaterial;

        public bool snapped { get; private set; }

        private Collider collider;
        private InteractiveItem interactiveItem;
        private SnappingItem snappingItem;
        private GameObject placeholderObject;

        #endregion

        private void Awake()
        {
            collider = GetComponent<Collider>();
            interactiveItem = GetComponent<InteractiveItem>();
        }

        private void Start()
        {
            collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SnappingItem>() && !snapped)
            {
                var sO = other.GetComponent<SnappingItem>();

                if (SnappingValid(sO.objectName))
                {
                    snappingItem = sO;

                    snappingItem.AssignSnappingCollider(this);

                    if (placeholderObject == null)
                    {
                        CreateGhostMesh(snappingItem.gameObject);
                    }
                    
                    ToggleGhostMesh(true);
                }               
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.gameObject == snappingItem && !snapped)
            if (other.gameObject == snappingItem.gameObject && snappingItem != null)
            {
                snappingItem.AssignSnappingCollider(null);

                DisableSnap();
            }
        }

        public void ToggleGhostMesh(bool _show)
        {            
            placeholderObject.SetActive(_show);            
        }

        public void ObjectSnapped()
        {
            snapped = true;
            interactiveItem.OnItemUsed();
            ToggleGhostMesh(false);
        }

        public void DisableSnap()
        {
            snapped = false;
            ToggleGhostMesh(false);
            snappingItem = null;
        }

        private void CreateGhostMesh(GameObject _enteringObject)
        {
            placeholderObject = Instantiate(_enteringObject) as GameObject;

            placeholderObject.transform.parent = transform;

            placeholderObject.transform.localPosition = Vector3.zero;
            placeholderObject.transform.localRotation = transform.rotation;

            //placeholderObject = CombineMeshes.Combine(_enteringObject, transform, ghostMaterial);           
        }

        private bool SnappingValid(string _enteringObjectName)
        {
            for (int i = 0; i < objectsToSnap.Count; i++)
            {
                if (_enteringObjectName == objectsToSnap[i])
                {
                    return true;
                }
            }

            return false;
        }
    }
}


