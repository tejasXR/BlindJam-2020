using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.BlindJam
{
    public class StartPoint : MonoBehaviour
    {
        #region EVENTS

        public static event Action StartedCallback;

        protected void OnStarted()
        {
            if (StartedCallback != null)
            {
                StartedCallback?.Invoke();
            }
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                // Call StartedCallback when we interact with a hand object
                OnStarted();

                gameObject.SetActive(false);
            }
        }
    }
}


