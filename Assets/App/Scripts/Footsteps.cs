using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.BlindJam
{
    public class Footsteps : MonoBehaviour
    {

        [SerializeField] float footstepDistanceThreshold;
        [SerializeField] AudioSource footStepAudioSource;
        [SerializeField] AudioClip[] footstepClips;

        private Vector3 origin;
        private bool footstepsOn;
        private Player player;

        private void Start()
        {
            player = Player.Instance;
        }

        private void OnEnable()
        {
            StartPoint.StartedCallback += EnableFootsteps;
        }

        private void OnDisable()
        {
            StartPoint.StartedCallback -= EnableFootsteps;
        }

        void Update()
        {
            // If footsteps are enabled
            if (footstepsOn && player.GetPlayerAlive())
            {
                // If we cross a distance threshold
                if (Vector3.Distance(origin, transform.position) > footstepDistanceThreshold)
                {
                    TakeStep();
                }
            }           
        }

        private void EnableFootsteps()
        {
            footstepsOn = true;
            origin = transform.position;
        }

        private void TakeStep()
        {
            

            // Re-orient origin
            origin = transform.position;

            // Play random footstep clip
            var randomClip = footstepClips[Utility.GetRandomInt(0, footstepClips.Length)];
            footStepAudioSource.PlayOneShot(randomClip);

            Debug.Log("Taking footstep");
        }
    }
}


