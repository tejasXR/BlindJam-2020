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

        private void Start()
        {
            GetXRNode();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Haptics 1"))
            {
                PlayerHaptics.SendHaptics(xrNode, haptics1Amplitude, .1F);
            }
            else if (other.CompareTag("Haptics 2"))
            {
                PlayerHaptics.SendHaptics(xrNode, haptics2Amplitude, .1F);
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
