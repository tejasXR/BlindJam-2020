using UnityEngine;
using UnityEngine.EventSystems;
using APERION.VR.INTERACTIVE;

namespace APERION.VR
{

    public class HandPointer : MonoBehaviour
    {
        public LayerMask raycastLayer;
        
        [Space(7)]
        public LineRenderer pointer;
        public bool showPointerOnlyOnLayer;
        public Material pointerMateialOverItem;

        [Space(7)]
        public bool curvedUI;

        private InteractiveItem interactiveItem;
        private Material pointerMaterial;

        private void Start()
        {
            pointerMaterial = pointer.material;
        }

        private void Update()
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            {
                // Do something with UI
            }

            if (curvedUI)
            {
                CurvedUIInputModule.CustomControllerRay = new Ray(transform.position, transform.forward);                
            }
            else
            {
                Ray ray = new Ray(pointer.transform.position, pointer.transform.forward);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, raycastLayer))
                {
                    if (showPointerOnlyOnLayer)
                    {
                        pointer.enabled = true;
                    }

                    if (pointerMateialOverItem != null)
                    {
                        TogglePointerColor(true);
                    }

                    if (interactiveItem == null)
                    {
                        InteractiveItemHoverEnter(raycastHit);
                    }
                    else
                    {
                        InteractiveItemHoverStay();
                    }
                }
                else
                {
                    if (showPointerOnlyOnLayer)
                    {
                        pointer.enabled = false;
                    }

                    TogglePointerColor(false);

                    InteractiveItemHoverExit();
                }
            }                                                  
        }

        public void PointerSelect()
        {
            Ray ray = new Ray(pointer.transform.position, pointer.transform.forward);
            RaycastHit rayHit;

            Debug.DrawRay(pointer.transform.position, pointer.transform.forward * 5F, Color.green, 6F);

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, raycastLayer))
            {
                var go = rayHit.collider.gameObject;

                if (go.GetComponent<InteractiveItem>())
                {
                    var interactiveItem = go.GetComponent<InteractiveItem>();
                    interactiveItem.OnItemUsed();
                }
            }
        }

        private void InteractiveItemHoverEnter(RaycastHit _hit)
        {
            var gO = _hit.collider.gameObject;

            if (gO.GetComponent<InteractiveItem>() != null && interactiveItem == null)
            {
                gO.GetComponent<InteractiveItem>().OnItemHoverEnter();
                interactiveItem = gO.GetComponent<InteractiveItem>();
            }
        }

        private void TogglePointerColor(bool _changeColor)
        {
            if(_changeColor)
            {
                pointer.material = pointerMateialOverItem;
            }
            else
            {
                pointer.material = pointerMaterial;
            }
        }

        private void InteractiveItemHoverStay()
        {
            interactiveItem.OnItemHoverStay();
        }

        private void InteractiveItemHoverExit()
        {
            if (interactiveItem != null)
            {
                interactiveItem.OnItemHoverExit();
                interactiveItem = null;
            }
        }
    }
}


