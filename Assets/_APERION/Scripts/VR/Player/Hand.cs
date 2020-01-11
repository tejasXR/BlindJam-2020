using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace APERION.VR
{
    [Serializable]
    public class HandActions
    {
        public HandActions() { }

        public HandActions(string _buttonName, PlayerInput.Inputs _buttonInput)
        {
            buttonName = _buttonName;
            buttonInput = _buttonInput;
        }

        public string buttonName;

        public PlayerInput.Inputs buttonInput;

        public UnityEvent actionDown;
        public UnityEvent actionPressed;
        public UnityEvent actionUp;        
    }

    public class Hand : MonoBehaviour
    {
        #region VARIABLES

        public enum HandOrientation
        {
            Left,
            Right
        }

        public HandOrientation handOrientation;
        
        [Space(7)]
        public List<HandActions> handActions = new List<HandActions>();

        public InputDevice device { get; private set; }
        public XRNode xrNode { get; private set; }

        private List<PlayerInput.Inputs> inputsToTrack = new List<PlayerInput.Inputs>();

        private bool triggerDown;
        private bool gripDown;
        private bool touchpadTouchDown;
        private bool touchpadClickDown;
        private bool primaryButtonDown;


        #endregion

        private void Awake()
        {
            SetDevice();

            for (int i = 0; i < handActions.Count; i++)
            {
                if (!inputsToTrack.Contains(handActions[i].buttonInput))
                {
                    inputsToTrack.Add(handActions[i].buttonInput);
                }
            }
        }

        private void OnEnable()
        {
            // Left Handed Event Subscriptions
            if (handOrientation == HandOrientation.Left)
            {
                PlayerInput.LTriggerDownCallback += TriggerDown;
                PlayerInput.LTriggerUpCallback += TriggerUp;

                PlayerInput.LGripDownCallback += GripDown;
                PlayerInput.LGripUpCallback += GripUp;

                PlayerInput.LTouchpadTouchDownCallback += TouchpadTouchDown;
                PlayerInput.LTouchpadTouchUpCallback += TouchpadTouchUp;
            }

            // Right Handed Event Subscriptions
            if (handOrientation == HandOrientation.Right)
            {
                PlayerInput.RTriggerDownCallback += TriggerDown;
                PlayerInput.RTriggerUpCallback += TriggerUp;

                PlayerInput.RGripDownCallback += GripDown;
                PlayerInput.RGripUpCallback += GripUp;

                PlayerInput.RTouchpadTouchDownCallback += TouchpadTouchDown;
                PlayerInput.RTouchpadTouchUpCallback += TouchpadTouchUp;
            }
        }

        private void OnDisable()
        {
            // Left Handed Event Subscriptions
            if (handOrientation == HandOrientation.Left)
            {
                PlayerInput.LTriggerDownCallback -= TriggerDown;
                PlayerInput.LTriggerUpCallback -= TriggerUp;

                PlayerInput.LGripDownCallback -= GripDown;
                PlayerInput.LGripUpCallback -= GripUp;

                PlayerInput.LTouchpadTouchDownCallback -= TouchpadTouchDown;
                PlayerInput.LTouchpadTouchUpCallback -= TouchpadTouchUp;
            }

            // Right Handed Event Subscriptions
            if (handOrientation == HandOrientation.Right)
            {
                PlayerInput.RTriggerDownCallback -= TriggerDown;
                PlayerInput.RTriggerUpCallback -= TriggerUp;

                PlayerInput.RGripDownCallback -= GripDown;
                PlayerInput.RGripUpCallback -= GripUp;

                PlayerInput.RTouchpadTouchDownCallback -= TouchpadTouchDown;
                PlayerInput.RTouchpadTouchUpCallback -= TouchpadTouchUp;
            }
        }

        private void SetDevice()
        {
            if (handOrientation == HandOrientation.Left)
            {
                device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
                xrNode = XRNode.LeftHand;
            }
            else
            {
                device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
                xrNode = XRNode.RightHand;
            }
        }

        #region TRIGGER

        private void TriggerDown()
        {
            for (int i = 0; i < handActions.Count; i++)
            {
                if (handActions[i].buttonInput == PlayerInput.Inputs.Trigger)
                {
                    handActions[i].actionDown.Invoke();
                }                
            }

            triggerDown = true;
        }

        private void TriggerPressed()
        {

        }

        private void TriggerUp()
        {
            for (int i = 0; i < handActions.Count; i++)
            {
                if (handActions[i].buttonInput == PlayerInput.Inputs.Trigger)
                {
                    handActions[i].actionUp.Invoke();
                }
            }

            triggerDown = false;
        }

        #endregion

        #region GRIP

        private void GripDown()
        {
            for (int i = 0; i < handActions.Count; i++)
            {
                if (handActions[i].buttonInput == PlayerInput.Inputs.Grip)
                {
                    handActions[i].actionDown.Invoke();
                }
            }

            gripDown = true;
        }

        private void GripUp()
        {
            gripDown = false;
        }

        #endregion

        #region TOUCHPAD TOUCH

        // Touchpad Touch Methods
        private void TouchpadTouchDown()
        {
            for (int i = 0; i < handActions.Count; i++)
            {
                if (handActions[i].buttonInput == PlayerInput.Inputs.TouchpadTouch)
                {
                    handActions[i].actionDown.Invoke();
                }
            }

            touchpadTouchDown = true;
        }

        private void TouchpadTouchUp()
        {
            for (int i = 0; i < handActions.Count; i++)
            {
                if (handActions[i].buttonInput == PlayerInput.Inputs.TouchpadTouch)
                {
                    handActions[i].actionUp.Invoke();
                }
            }

            touchpadTouchDown = false;
        }

        #endregion

        #region TOUCHPAD CLICK

        #endregion

        #region TOUCHPAD AXIS

        #endregion

        #region PRIMARY BUTTON

        #endregion

        #region INSPECTOR METHODS

        public void CreateBasicHandActions()
        {
            handActions.Clear();

            handActions.Add(new HandActions("Trigger", PlayerInput.Inputs.Trigger));
            handActions.Add(new HandActions("Grip", PlayerInput.Inputs.Grip));
            handActions.Add(new HandActions("Touchpad Touch", PlayerInput.Inputs.TouchpadTouch));
            handActions.Add(new HandActions("Touchpad Click", PlayerInput.Inputs.TouchpadClick));
            handActions.Add(new HandActions("Primary Button", PlayerInput.Inputs.PrimaryButton));
        }

        #endregion               
    }
}


