using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    // A script which plays an animation when an InteractiveItem is used

    [RequireComponent(typeof(InteractiveItem))]
    public class InteractiveAnimation : MonoBehaviour
    {
        public Animator animator;

        public string hoverEnterTrigger;
        public string itemUsedTrigger;
        public string itemUnusedTrigger;
        public string hoverExitTrigger;
        public string animationFadeOutTrigger;

        public string stateToRestartAt;

        private InteractiveItem interactiveItem;

        private void Awake()
        {
            interactiveItem = GetComponent<InteractiveItem>();
        }

        private void OnEnable()
        {
            interactiveItem.ItemHoverEnterCallback += PlayEnterAnimation;
            interactiveItem.ItemUsedCallback += PlayUsedAnimation;
            interactiveItem.ItemHoverExitCallback += PlayExitAnimation;

            //RestartAnimationCycle();
        }

        private void OnDisable()
        {
            interactiveItem.ItemHoverEnterCallback -= PlayEnterAnimation;
            interactiveItem.ItemUsedCallback -= PlayUsedAnimation;
            interactiveItem.ItemHoverExitCallback -= PlayExitAnimation;

            //PlayFadeOutAnimation();
        }
        
        // Plays an animation triggers from an animator
        private void PlayUsedAnimation()
        {
            if (animator != null && itemUsedTrigger != null)
            {
                ClearTriggers();

                animator.SetTrigger(itemUsedTrigger);
            }            
        }

        private void PlayUnusedAnimation()
        {
            if (animator != null && itemUnusedTrigger != null)
            {
                ClearTriggers();

                animator.SetTrigger(itemUnusedTrigger);
            }
        }

        private void PlayEnterAnimation()
        {
            if (animator != null && hoverEnterTrigger != null)
            {
                ClearTriggers();

                animator.SetTrigger(hoverEnterTrigger);
            }
        }

        private void PlayExitAnimation()
        {
            if (animator != null && hoverExitTrigger != null)
            {
                ClearTriggers();

                animator.SetTrigger(hoverExitTrigger);
            }
        }

        private void PlayFadeOutAnimation()
        {
            if (animator != null && animationFadeOutTrigger != null)
            {
                ClearTriggers();

                animator.SetTrigger(animationFadeOutTrigger);
            }
        }

        private void RestartAnimationCycle()
        {
            if (animator != null  && stateToRestartAt != null)
            {
                animator.Play(stateToRestartAt, 0);
            }
        }

        private void ClearTriggers()
        {
            animator.ResetTrigger(itemUsedTrigger);
            animator.ResetTrigger(itemUnusedTrigger);
            animator.ResetTrigger(hoverExitTrigger);
            animator.ResetTrigger(hoverEnterTrigger);
        }
    }
}