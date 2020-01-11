using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR
{
    public class HandModel : MonoBehaviour
    {
        public enum HandOrientation
        {
            Left,
            Right
        }

        public enum HandFollowType
        {
            Fixed,
            Lerp,
            Rigidbody
        }

        [SerializeField] HandOrientation handOrientation;
        [SerializeField] HandFollowType handFollowType;

        private GameObject handToFollow;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            FindHandObject();

            if (handFollowType == HandFollowType.Fixed)
            {
                transform.parent = handToFollow.transform;
            }
        }

        private void Update()
        {
            switch (handFollowType)
            {
                case HandFollowType.Lerp:
                    // Lerp follow appropriate hand
                    transform.position = Utility.Vector3Lerp(transform.position, handToFollow.transform.position, 30F);
                    break;
                case HandFollowType.Rigidbody:
                    MoveRigidbody();
                    break;
            }        
        }

        private void FindHandObject()
        {
            // Get hand via switch statement and enum declaration
            switch (handOrientation)
            {
                case HandOrientation.Left:
                    handToFollow = PlayerManager.Instance.leftHand;
                    break;

                case HandOrientation.Right:
                    handToFollow = PlayerManager.Instance.rightHand;
                    break;
            }
        }

        private void MoveRigidbody()
        {
            Vector3 destination = handToFollow.transform.position - transform.position;
            destination = destination.normalized * Time.deltaTime;

            rb.MovePosition(transform.position + destination);
        }
    }
}

