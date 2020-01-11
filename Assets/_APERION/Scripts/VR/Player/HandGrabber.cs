using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using APERION.VR.INTERACTIVE;

namespace APERION.VR
{
    [RequireComponent(typeof(Hand))]
    public class HandGrabber : MonoBehaviour
    {        
        public bool hideHandOnGrab;
        public GameObject handModel;

        private Hand hand;
        private GrabbableItem grabbableItem;

        private void Awake()
        {
            hand = GetComponent<Hand>();
        }

        public Hand GetHand()
        {
            return hand;
        }

        public void SwitchObjectToOtherHand()
        {
            ShowHand();
            grabbableItem = null;
        }

        public void Grab()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, .05F);

            foreach (var go in colliders)
            {
                if (grabbableItem == null)
                {
                    if (go.GetComponent<GrabbableItem>() && go.GetComponent<GrabbableItem>().enabled)
                    {
                        grabbableItem = go.GetComponent<GrabbableItem>();

                        grabbableItem.SetGrabbedHand(this);
                        grabbableItem.GetComponent<InteractiveItem>().OnItemUsed();

                        //HapticsManager.Instance.HapticPulse(.1F, .35F, hand.device);

                        HideHand();

                        return;
                    }
                }                
            }
        }

        public void Ungrab()
        {
            if (grabbableItem != null)
            {
                grabbableItem.GetComponent<InteractiveItem>().OnItemUnused();

                grabbableItem = null;

                ShowHand();
            }
        }

        public Vector3 GetControllerPositionVelocity()
        {
            List<XRNodeState> nodes = new List<XRNodeState>();

            InputTracking.GetNodeStates(nodes);

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].nodeType == hand.xrNode)
                {
                    Vector3 vel = new Vector3();
                    nodes[i].TryGetVelocity(out vel);
                    return vel;
                }
            }
            
            return Vector3.zero;
        }

        public Vector3 GetControllerAngularVelocity()
        {
            List<XRNodeState> nodes = new List<XRNodeState>();

            InputTracking.GetNodeStates(nodes);

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].nodeType == hand.xrNode)
                {
                    Vector3 angVel = new Vector3();
                    nodes[i].TryGetAngularVelocity(out angVel);
                    return angVel;
                }
            }

            return Vector3.zero;
        }

        private void HideHand()
        {
            if (hideHandOnGrab)
            {
                handModel.SetActive(false);
            }
        }

        private void ShowHand()
        {
            if (hideHandOnGrab)
            {
                handModel.SetActive(true);
            }
        }    
    }
}


