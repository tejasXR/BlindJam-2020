using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace APERION.VR.INTERACTIVE
{
    // A script which calls UnityActions when an InteractiveItem is used

    [RequireComponent(typeof(InteractiveItem))]    
    public class InteractiveAction : MonoBehaviour
    {
        public UnityEvent hoverEnterAction;
        public UnityEvent hoverStayAction;
        public UnityEvent hoverExitAction;

        public UnityEvent usedAction;
        public UnityEvent unusedAction;

        private InteractiveItem interactiveItem;

        private void Awake()
        {
            interactiveItem = GetComponent<InteractiveItem>();
        }

        private void OnEnable()
        {
            interactiveItem.ItemHoverEnterCallback += InvokeHoverEnterAction;
            interactiveItem.ItemHoverStayCallback += InvokeHoverStayAction;
            interactiveItem.ItemHoverExitCallback += InvokeHoverExitAction;

            interactiveItem.ItemUsedCallback += InvokeUsedAction;
            interactiveItem.ItemUnusedCallback += InvokeUnusedAction;
        }

        private void OnDisable()
        {
            interactiveItem.ItemHoverEnterCallback -= InvokeHoverEnterAction;
            interactiveItem.ItemHoverStayCallback -= InvokeHoverStayAction;
            interactiveItem.ItemHoverExitCallback -= InvokeHoverExitAction;

            interactiveItem.ItemUsedCallback -= InvokeUsedAction;
            interactiveItem.ItemUnusedCallback -= InvokeUnusedAction;
        }

        private void InvokeHoverEnterAction()
        {
            if (hoverEnterAction != null)
                hoverEnterAction.Invoke();
        }

        private void InvokeHoverStayAction()
        {
            if (hoverStayAction != null)
                hoverStayAction.Invoke();
        }

        private void InvokeHoverExitAction()
        {
            if (hoverExitAction != null)
                hoverExitAction.Invoke();
        }

        private void InvokeUsedAction()
        {
            if (usedAction != null)
                usedAction.Invoke();
        }

        private void InvokeUnusedAction()
        {
            if (unusedAction != null)
                unusedAction.Invoke();
        }
    }
}


