using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR
{
    public class TeleportPoint : MonoBehaviour
    {
        private bool teleporting;
        private FadeTransition fadeTransition;

        private void Awake()
        {
            // Optimize
            if (FindObjectOfType<FadeTransition>() == null)
            {
                Debug.LogError("There is no FadeTransition object in the scene!");
            }
            else
            {
                fadeTransition = FindObjectOfType<FadeTransition>();
            }
        }

        // Teleports player to designated location
        public void Teleport()
        {
            if (!teleporting)
            {
                StartCoroutine(TeleportTransition());
            }           
        }

        private IEnumerator TeleportTransition()
        {
            teleporting = true;

            // Fade Out
            fadeTransition.TeleportTransition(FadeTransition.FadeType.FadeOut);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            // Orients player
            PlayerManager.Instance.SetPlayerPosition(transform);
            PlayerManager.Instance.SetPlayerRotation(transform);

            // Fades In
            fadeTransition.TeleportTransition(FadeTransition.FadeType.FadeIn);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            teleporting = false;
        }
    }
}


