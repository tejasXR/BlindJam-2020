using APERION.VR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace APERION.BlindJam
{
    /// <summary>
    /// A script attached to a player's hand to sense walls
    /// </summary>
    public class WallSensor : MonoBehaviour
    {
        public enum HandOrientation
        {
            Left,
            Right
        }

        [SerializeField] HandOrientation handOrientation;

        [SerializeField] float haptics1Amplitude;
        [SerializeField] float haptics2Amplitude;

        private XRNode xrNode;
        private bool hapticsOn;
        //private bool haptics2On;

        private void Start()
        {
            GetXRNode();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Haptics 1") && !hapticsOn)
            {
                hapticsOn = true;
                StartCoroutine(HapticsPulseRepeater(haptics1Amplitude, .01F, .4F));
            }
            else if (other.CompareTag("Haptics 2"))
            {
                //hapticsOn = false;
                //haptics2On = true;
                StartCoroutine(HapticsPulseRepeater(haptics1Amplitude, .05F, .2F));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //if (other.CompareTag("Haptics 1"))
            //{
            //    hapticsOn = true;
            //    StartCoroutine(HapticsPulseRepeater(haptics1Amplitude, .01F, .5F));
            //    //PlayerHaptics.SendHaptics(xrNode, haptics1Amplitude, .1F);
            //}
            //else if (other.CompareTag("Haptics 2"))
            //{
            //    //PlayerHaptics.SendHaptics(xrNode, haptics2Amplitude, .1F);
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Haptics 1"))
            {
                hapticsOn = false;
                StopHaptics();

            }
            if (other.CompareTag("Haptics 2"))
            {
                // Player dies
            }
        }

        //private void EnableHaptics1()
        //{

        //}

        //private void EnableHaptics2()
        //{

        //}

        private void StopHaptics()
        {
            PlayerHaptics.StopHaptics(xrNode);
        }

        private IEnumerator HapticsPulseRepeater(float _amplitude, float _duration, float _frequency)
        {
            // Reset haptics
            StopHaptics();

            while (hapticsOn)// || haptics2On)
            {
                PlayerHaptics.SendHaptics(xrNode, _amplitude, _duration);

                yield return new WaitForSeconds(_frequency);
            }
        }




        private void GetXRNode()
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
    }



}
