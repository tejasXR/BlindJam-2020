using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using APERION;

namespace APERION.VR.INTERACTIVE
{
    // A script that attaches to an VR interactive object

    [RequireComponent(typeof(Collider))]
    public class InteractiveItem : MonoBehaviour
    {
        public event Action ItemHoverEnterCallback;
        public event Action ItemHoverStayCallback;
        public event Action ItemHoverExitCallback;
        public event Action ItemUsedCallback;
        public event Action ItemUnusedCallback;

        public bool shouldDebug;
      
        public virtual void OnItemHoverEnter()
        {
            ItemHoverEnterCallback?.Invoke();

            if (shouldDebug)
                Debug.Log("Hover Enter");
        }

        public virtual void OnItemHoverStay()
        {
            ItemHoverStayCallback?.Invoke();

            if (shouldDebug)
                Debug.Log("Hover Stay");
        }

        public virtual void OnItemHoverExit()
        {
            ItemHoverExitCallback?.Invoke();

            if (shouldDebug)
                Debug.Log("Hover Exit");
        }

        public virtual void OnItemUsed()
        {
            ItemUsedCallback?.Invoke();

            if (shouldDebug)
                Debug.Log("Item Used");
        }

        public virtual void OnItemUnused()
        {
            ItemUnusedCallback?.Invoke();

            if (shouldDebug)
                Debug.Log("Item Unused");
        }
    }
}


