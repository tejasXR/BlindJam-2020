using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using APERION;

namespace APERION.VR.INTERACTIVE
{
    // A script which create a 'hotspot' like area sensitive to a player's gaze
    
    [RequireComponent(typeof(InteractiveItem))]
    public class Hotspot : MonoBehaviour
    {       
        // An enumumeration to set what the player forward direction should be
        public enum PlayerGazeObject
        {
            HeadsetDirection
        }

        public enum LookAtDirection
        {
            PlayerHead,
            VectorZero,
            Self
        }

        [Tooltip("The angle threshold between the player's gaze and the object forward direction in which the hotspot will be marked as 'hot'")]
        public float angleThreshold;

        [Tooltip("The distance between the player and the object used for activation")]
        public float distanceThreshold;

        [Tooltip("The object we want to have the player look at. Leave empty if we want the player to look at this object")]
        public GameObject gazeTarget;

        [Tooltip("An enumumeration to set what object is responsible for the players forward gaze")]
        public PlayerGazeObject playerGazeObject;

        [Tooltip("An enumumeration to set what object this object should look at")]
        public LookAtDirection lookAtDirection;

        [Space(7)]
        public UnityEvent hoverEnter;
        public UnityEvent hoverStay;
        public UnityEvent hoverExit;

        private InteractiveItem interactiveItem;
        private Vector3 lookAtVector;
        private GameObject player;
        private bool hotspotEntered = false;

        // The angle difference between the players gaze and the hotspot responders forward direction
        private float angleDifference;


        private void Awake()
        {
            interactiveItem = GetComponent<InteractiveItem>();
        }

        private void OnEnable()
        {
            interactiveItem.ItemHoverEnterCallback += HoverEnter;
            interactiveItem.ItemHoverStayCallback += HoverStay;
            interactiveItem.ItemHoverExitCallback += HoverExit;
        }

        private void OnDisable()
        {
            interactiveItem.ItemHoverEnterCallback -= HoverEnter;
            interactiveItem.ItemHoverStayCallback -= HoverStay;
            interactiveItem.ItemHoverExitCallback -= HoverExit;
        }

        void Start()
        {
            // Get the 'player' object that wil control the gaze direction (i.e., headset or controller)
            GetPlayer();
        }

        private void Update()
        {
            GetGazeTarget();
            GetAngleDifference();
            GetLookDirection();
            CheckAngleDifference();            
        }

        // Gets the appropriate player object based on the given gaze option
        private void GetPlayer()
        {
            switch (playerGazeObject)
            {
                case PlayerGazeObject.HeadsetDirection:
                    player = PlayerManager.Instance.playerHead;
                    break;
            }
        }

        private void GetLookDirection()
        {
            switch (lookAtDirection)
            {
                case LookAtDirection.PlayerHead:
                    lookAtVector = PlayerManager.Instance.playerHead.transform.position;
                    break;

                case LookAtDirection.VectorZero:
                    lookAtVector = Vector3.zero;
                    break;

                case LookAtDirection.Self:
                    lookAtVector = gazeTarget.transform.position;
                    break;
            }

            gazeTarget.transform.LookAt(lookAtVector);
        }

        private void GetGazeTarget()
        {
            if (gazeTarget == null)
            {
                gazeTarget = gameObject;
            }
        }

        // Gets the angle difference between the cameras forward direction and the forward direction of this object (not that flexible, but written as a starting point)
        private float GetAngleDifference()
        {
            Vector3 playerForwardDirection = player.transform.forward;

            Ray ray = new Ray(gazeTarget.transform.position, (gazeTarget.transform.position - player.transform.position));

            // We want to take the inverse of the direction to see when the player's gaze is facing the object
            angleDifference = Vector3.Angle(playerForwardDirection, ray.direction);

            return angleDifference;
        }

        private float GetDistance()
        {
            return Vector3.Distance(gazeTarget.transform.position, player.transform.position);
        }

        private void CheckAngleDifference()
        {         
            if (GetAngleDifference() < angleThreshold && GetDistance() < distanceThreshold)
            {
                if (!hotspotEntered)
                {
                    HoverEnter();
                    hotspotEntered = true;
                }
                else
                {
                    HoverStay();
                }
            }
            else
            {
                if (hotspotEntered)
                {
                    HoverExit();
                    hotspotEntered = false;
                }
            }            
        }

        private void HoverEnter()
        {
            hoverEnter.Invoke();            
        }

        private void HoverStay()
        {
            hoverStay.Invoke();
        }

        private void HoverExit()
        {
            hoverExit.Invoke();
        }

        /*

        // An enumumeration to set what the player forward direction should be
        public enum PlayerGazeObject
        {
            HeadsetDirection,
            ControllerDirection,
        }

        public enum LookAtDirection
        {
            PlayerHead,
            PlayerController,
            VectorZero,
            Self
        }

        [Tooltip("The angle threshold between the player's gaze and the object forward direction in which the hotspot will be marked as 'hot'")]
        public float angleThreshold;

        [Tooltip("An enumumeration to set what object is responsible for the players forward gaze")]
        public PlayerGazeObject playerGazeObject;

        [Tooltip("An enumumeration to set what object this object should look at")]
        public LookAtDirection lookAtDirection;

        private GameObject player;
        private Vector3 lookAtVector;
        private InteractiveItem interactiveItem;
        private bool hotspotEntered = false;
        private bool hotspotTransitioning = false;

        // The angle difference between the players gaze and the hotspot responders forward direction
        private float angleDifference;

        private void Awake()
        {
            interactiveItem = GetComponent<InteractiveItem>();
        }
   
        void Start()
        {
            // Get the 'player' object that wil control the gaze direction (i.e., headset or controller)
            GetPlayer();      
        }

        void Update()
        {
            GetAngleDifference();
            GetLookDirection();
            CheckAngleDifference();
        }

        // Gets the angle difference between the cameras forward direction and the forward direction
        // of this object (not that flexible, but written as a starting point)
        public float GetAngleDifference()
        {
            Vector3 playerForwardDirection = player.transform.forward;

            Ray ray = new Ray(transform.position, (transform.position - player.transform.position));
         
            // We want to take the inverse of the direction to see when the player's gaze is facing the object
            angleDifference = Vector3.Angle(playerForwardDirection, ray.direction);

            return angleDifference;
        }

        private void CheckAngleDifference()
        {
            if (!hotspotTransitioning)
            {
                if (GetAngleDifference() < angleThreshold)
                {
                    if (!hotspotEntered)
                    {
                        HotspotEnter();
                        hotspotEntered = true;
                    }
                }
                else
                {
                    if (hotspotEntered)
                    {
                        HotspotExit();
                        hotspotEntered = false;
                    }
                }
            }           
        }

        // Gets the appropriate player object based on the given gaze option
        private void GetPlayer()
        {
            switch (playerGazeObject)
            {
                case PlayerGazeObject.HeadsetDirection:
                    player = PlayerManager.Instance.playerHead;
                    break;

                case PlayerGazeObject.ControllerDirection:
                    player = PlayerManager.Instance.rightController;
                    break;
            }
        }

        private void GetLookDirection()
        {
            switch (lookAtDirection)
            {
                case LookAtDirection.PlayerHead:
                    lookAtVector = PlayerManager.Instance.playerHead.transform.position;
                    break;

                case LookAtDirection.PlayerController:
                    lookAtVector = PlayerManager.Instance.rightController.transform.position;
                    break;
                case LookAtDirection.VectorZero:
                    lookAtVector = Vector3.zero;
                    break;

                case LookAtDirection.Self:
                    lookAtVector = transform.position;
                    break;
            }

            transform.LookAt(lookAtVector);
        }

        public bool GetHotspotEntered()
        {
            if (hotspotEntered)
                return true;
            else
                return false;
        }

        private void HotspotDisable()
        {

        }

        public void HotspotEnter()
        {
            interactiveItem.OnItemHoverEnter();
        }

        public void HotspotUsed()
        {
            StartCoroutine(HotspotEnterRoutine());
        }

        public void HotspotExit()
        {
            StartCoroutine(HotspotExitRoutine());

        }

        private IEnumerator HotspotEnterRoutine()
        {
            hotspotTransitioning = true;

            if (GetComponent<HotspotDelay>())
            {
                var hotspotDelay = GetComponent<HotspotDelay>();

                if (hotspotDelay.hoverEnterDelay > 0)
                    yield return new WaitForSeconds(hotspotDelay.hoverEnterDelay);
            }
           
            interactiveItem.OnItemHoverExit();

            hotspotTransitioning = false;
        }

        private IEnumerator HotspotExitRoutine()
        {
            hotspotTransitioning = true;

            if (GetComponent<HotspotDelay>())
            {
                var hotspotDelay = GetComponent<HotspotDelay>();

                if (hotspotDelay.hoverExitDelay > 0)
                    yield return new WaitForSeconds(hotspotDelay.hoverExitDelay);
            }

            interactiveItem.OnItemHoverExit();

            hotspotTransitioning = false;

        }*/
    }

}


