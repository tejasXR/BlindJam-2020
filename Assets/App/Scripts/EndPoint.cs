using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.BlindJam
{
    public class EndPoint : MonoBehaviour
    {
        #region EVENTS

        public static event Action EndedCallback;

        protected void OnEnded()
        {
            if (EndedCallback != null)
            {
                EndedCallback?.Invoke();
            }
        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                // Call StartedCallback when we interact with a hand object
                OnEnded();

                gameObject.SetActive(false);
            }
        }
    }
}


