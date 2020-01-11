using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APERION;

namespace APERION.VR
{
    // A script that allows an object to follow the player's view

    public class FollowCameraView : MonoBehaviour
    {       
        public float distanceFromPlayer;

        public enum FollowType
        {
            Direct,
            Smooth,
            Threshold
        }

        public FollowType followType;
        public float angleThreshold;
        public float followSpeed;

        private Vector3 angleThresholdPlaceholder;
        private float angleFromView;

        private void Start()
        {
            angleThresholdPlaceholder = transform.position;
        }

        void Update()
        {
            FollowView();
        }

        private Vector3 GetPlayerForwardLocation(float distFromPlayer)
        {          
            Ray ray = new Ray(PlayerManager.Instance.playerHead.transform.position, GetPlayerForwardDirection());

            Debug.DrawRay(PlayerManager.Instance.playerHead.transform.position, GetPlayerForwardDirection(), Color.green);

            var newPos = ray.GetPoint(distFromPlayer);

            newPos.y = 0;

            return newPos;
        }

        private Vector3 GetPlayerForwardDirection()
        {
            var direction = PlayerManager.Instance.playerHead.transform.forward;

            direction.y = 0;

            return direction;
        }

        private Vector3 GetPlayerPosition()
        {
            return PlayerManager.Instance.playerHead.transform.position;
        }

        private void FollowView()
        {
            switch (followType)
            {
                case FollowType.Direct:
                    transform.position = GetPlayerForwardLocation(distanceFromPlayer);
                    break;

                case FollowType.Smooth:
                    transform.position = Vector3.Lerp(transform.position, GetPlayerForwardLocation(distanceFromPlayer), Time.deltaTime * followSpeed);
                    break;

                case FollowType.Threshold:
                    GetAngleDifference();
                    CheckAngleThreshold();
                    transform.position = Vector3.Lerp(transform.position, angleThresholdPlaceholder, Time.deltaTime * followSpeed);
                    break;
            }          

            var lookPos = GetPlayerPosition() - transform.position;

            lookPos.y = 0;

            var rotation = Quaternion.LookRotation(lookPos);

            transform.rotation = rotation;
        }

        private float GetAngleDifference()
        {
            var pos = new Vector3(transform.position.x, 0F, transform.position.z);
            var playerPos = PlayerManager.Instance.playerHead.transform.position;

            var playerToObject = pos - playerPos;

            angleFromView = Vector3.Angle(playerToObject, GetPlayerForwardDirection());

            return angleFromView;
        }

        private void CheckAngleThreshold()
        {
            if (angleFromView >= angleThreshold)
            {
                angleThresholdPlaceholder = GetPlayerForwardLocation(distanceFromPlayer);
            }
        }
    }
}


