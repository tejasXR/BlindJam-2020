using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace APERION.VR
{
    // Teleports the player to different locations
     
    public class Teleport : MonoBehaviour
    {
        /*
        public LayerMask excludeLayers;
        //public LayerMask surroundingGround;
        public float maxTeleportDistance = 15F;
        public GameObject teleportMarker;
        public float invalidMarkerOpacity = 0.5F;

        public GameObject playerHand;

        private bool isActive;
        private bool isFirstPressed;


        //private LineRenderer lineRenderer;

        private int teleportState;
        // 0 = hidden
        // 1 = transparent
        // 2 = opaque

        private void OnEnable()
        {
            // Hard coded to left hand for now!!!
            PlayerInputQuest.LTriggerPressedCallback += TeleportActive;
            PlayerInputQuest.LTriggerUpCallback += TeleportPlayer;
        }


        private void OnDisable()
        {
            PlayerInputQuest.LTriggerPressedCallback -= TeleportActive;
            PlayerInputQuest.LTriggerUpCallback -= TeleportPlayer;


        }

        void Start()
        {
            teleportMarker.SetActive(false);
            //lineRenderer = GetComponentInChildren<LineRenderer>();
            //playerHand = PlayerManager.Instance.rightController;

            isFirstPressed = true;

        }

        void Update()
        {
            if (!isActive)
                return;

            SetLineDistance();

            //AdjustTeleportState();

        }



        private void TeleportActive()
        {
            //if (teleportState > 0)
                isActive = true;
            //SetLineDistance();
        }

        private void SetLineDistance()
        {
            //lineRenderer.SetPosition(0, playerHand.transform.position);
            //lineRenderer.SetPosition(1, playerHand.transform.forward * 5F);

            Ray ray = new Ray(playerHand.transform.position, playerHand.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxTeleportDistance, ~excludeLayers))
            {

                if (isFirstPressed)
                    SetTeleportMarker(hit.point, false);
                else
                    SetTeleportMarker(hit.point, true);


                if (hit.collider.CompareTag("DestinationPoint"))
                {
                    teleportState = 2;
                }
                else
                {
                    teleportState = 1;
                }

                //if (Physics.Raycast(ray, out hit, maxTeleportDistance, ~excludeLayers))
                //{
                //    validTeleport = true;
                //} else
                //{
                //    validTeleport = false;
                //}

                //print("Found Teleport Area");

                //var hitLocation = hit.point;
                //Vector3.Distance(ray.origin, hitLocation);


            }else
            {
                teleportState = 0;
                //lineRenderer.SetPosition(1, playerHand.transform.forward * 10.0F);

            }
        }

        private void AdjustTeleportState()
        {
            switch (teleportState)
            {
                case 0:
                    HideMarker();
                    break;
                case 1:
                    InvalidMarker();
                    break;
                case 2:
                    ValidMarker();
                    break;
            }
        }

        private void SetTeleportMarker(Vector3 markerPos, bool lerp)
        {
            teleportMarker.gameObject.SetActive(true);

            if (lerp)
                teleportMarker.transform.position = Vector3.Lerp(teleportMarker.transform.position, markerPos, Time.deltaTime * 5F);
            else
                teleportMarker.transform.position = markerPos;

            isFirstPressed = false;

            //teleportMarker.

            //Vector3 difference = PlayerManager.Instance.playerHead.transform.position - teleportMarker.transform.position;
            //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90.0F;
            //teleportMarker.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

            //transform.LookAt(PlayerManager.Instance.playerHead.transform.position);
            //var rot = new Vector3(0, 0, transform.rotation.eulerAngles.z);
            //teleportMarker.transform.rotation = rot;

            //Vector3 difference = PlayerManager.Instance.playerHead.transform.position - teleportMarker.transform.position;
            //difference.x = 0;
            //difference.y = 0;

            //var adjusted = PlayerManager.Instance.playerHead.transform.position;

            //adjusted.x = 0;
            //adjusted.y = 0;

            //teleportMarker.transform.LookAt(adjusted, transform.forward);


            //Quaternion lookAngle = Quaternion.LookRotation(difference, transform.up);

            //teleportMarker.transform.rotation = lookAngle;

        }


        private void ValidMarker()
        {
            //var tempColor = teleportMarker.color;
            //tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime * 5F);
            //teleportMarker.color = tempColor;
        }

        private void InvalidMarker()
        {
            //var tempColor = teleportMarker.color;
            //tempColor.a = Mathf.Lerp(tempColor.a, invalidMarkerOpacity, Time.deltaTime * 5F);
            //teleportMarker.color = tempColor;
        }

        private void HideMarker()
        {
            //var tempColor = teleportMarker.color;
            //tempColor.a = Mathf.Lerp(tempColor.a, 0F, Time.deltaTime * 5F);
            //teleportMarker.color = tempColor;

            teleportMarker.SetActive(false);
        }

        private void TeleportPlayer()
        {
            if (teleportState > 0)
            {
                var tempTeleport = teleportMarker.transform.position;

                tempTeleport.y = PlayerManager.Instance.playerBody.transform.position.y;

                PlayerManager.Instance.playerBody.transform.position = tempTeleport;

                teleportMarker.SetActive(false);

                isFirstPressed = true;
                isActive = false;
            }
        }
        */
    }
}


