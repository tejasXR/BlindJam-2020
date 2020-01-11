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

        private Vector3 exitPoint;
        private XRNode xrNode;
        private bool exited;
        private float distanceFromExitPoint;

        private void Start()
        {
            AssignHand();
        }

        private void Update()
        {
            if (exited)
            {
                //DynamicPulse();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
           
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
                if (!InsidePathObject())
                {
                    //ContactPoint contactPoint = other.GetContact(0);
                    exitPoint = transform.position;
                    exited = true;
                    StartCoroutine(HapticsPulseRepeater());
                }
              
            }
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.tag == "Path")
        //    {
        //        exited = false;
        //    }
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.tag == "Path")
        //    {
        //        ContactPoint contactPoint = collision.GetContact(0);
        //        exitPoint = contactPoint.point;
        //        exited = true;
        //    }            
        //}        

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
            // Reset haptics
            //StopHaptics();

            while (exited)// || haptics2On)
            {
                //PlayerHaptics.SendHaptics(xrNode, _amplitude, _duration);
                var amplitudeDist = Mathf.Clamp01(1 - ((DistanceFromExitPoint() / distanceThreshold)) + .3F);

                // Changing this so I preserve the haptic motors while developing
                if (DistanceFromExitPoint() >= distanceThreshold)
                {
                    amplitudeDist = 0;
                    //timeDist = 0;
                }

                PlayerHaptics.SendHaptics(xrNode, amplitudeDist, .1F);

                //yield return new WaitForSeconds(_frequency);

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

        //private IEnumerator HapticsPulseRepeater(float _amplitude, float _duration, float _frequency)
        //{
        //    // Reset haptics
        //    //StopHaptics();

        //    while (exited)// || haptics2On)
        //    {
        //        //PlayerHaptics.SendHaptics(xrNode, _amplitude, _duration);
        //        PlayerHaptics.SendHaptics(xrNode, DistanceFromExitPoint() / distanceThreshold, .1F);

        //        //yield return new WaitForSeconds(_frequency);
        //        yield return new WaitForSeconds(1- (DistanceFromExitPoint() / distanceThreshold));
        //    }
        //}
    }
}


