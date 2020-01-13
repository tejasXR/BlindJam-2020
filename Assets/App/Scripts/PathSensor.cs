using APERION.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace APERION.BlindJam
{
    public class PathSensor : MonoBehaviour
    {
        public enum HandOrientation
        {
            Left,
            Right
        }

        [SerializeField] HandOrientation handOrientation;
        [SerializeField] float distanceThreshold;
        [SerializeField] float exitCheckRadius;

        private Player player;
        private Vector3 exitPoint;
        private XRNode xrNode;
        private bool exited;
        private float distanceFromExitPoint;

        private void Start()
        {
            AssignHand();
            player = Player.Instance;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Path")
            {
                exited = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Path")
            {
                // Checks if we have truly exited any path
                if (!InsidePathObject())
                {
                    exitPoint = transform.position;
                    exited = true;
                    StartCoroutine(HapticsPulseRepeater());
                }
              
            }
        } 

        // Assigns a hand via Unity's XR System
        private void AssignHand()
        {
            switch (handOrientation)
            {
                case HandOrientation.Left:
                    xrNode = XRNode.LeftHand;
                    break;

                case HandOrientation.Right:
                    xrNode = XRNode.RightHand;
                    break;
            }
        }

        private void DynamicPulse()
        {
            PlayerHaptics.SendHaptics(xrNode, DistanceFromExitPoint() / distanceThreshold, .1F);
        }

        private float DistanceFromExitPoint()
        {
            distanceFromExitPoint = Vector3.Distance(exitPoint, transform.position);
            return distanceFromExitPoint;
        }

        private void StopHaptics()
        {
            PlayerHaptics.StopHaptics(xrNode);
        }

        private IEnumerator HapticsPulseRepeater()
        {
            // Only repeat coroutine if we've exited the path
            while (exited)
            {
                // Get the amplitude based on how far we are from crossing the distance threshold
                var amplitudeDist = Mathf.Clamp01(1 - ((DistanceFromExitPoint() / distanceThreshold)) + .3F);

                // Changing this so I preserve the haptic motors while developing
                if (DistanceFromExitPoint() >= distanceThreshold)
                {
                    amplitudeDist = 0;

                    player.PlayerDies();
                }

                PlayerHaptics.SendHaptics(xrNode, amplitudeDist, .1F);

                var timeDist = Mathf.Clamp01(1 - ((DistanceFromExitPoint() / distanceThreshold) +.5F));

                yield return new WaitForSeconds(timeDist);
            }
        }

        private bool InsidePathObject()
        {
            var colliders = Physics.OverlapSphere(transform.position, exitCheckRadius);

            foreach (var c in colliders)
            {
                if (c.gameObject.CompareTag("Path"))
                {
                    return true;
                }
            }

            return false;            
        }
    }
}


