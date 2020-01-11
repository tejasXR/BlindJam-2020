using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace APERION.VR
{
    /// <summary>
    /// A script that houses all Player input 
    /// </summary>
    // 

    // Generic input container holding button values
    [System.Serializable] 
    public class ButtonInput
    {
        public ButtonInput() { }

        public ButtonInput(InputDevice _device, InputFeatureUsage<bool> _usage)
        {
            device = _device;
            boolButton = _usage;
        }        

        public ButtonInput(InputDevice _device, InputFeatureUsage<float> _usage)
        {
            device = _device;
            floatButton = _usage;
        }
       
        public InputDevice device;
        public InputFeatureUsage<bool> boolButton;
        public InputFeatureUsage<float> floatButton;

        public bool boolValue;
        public float floatValue;
        public Vector2 vector2Value;
    }

    public class PlayerInput : MonoBehaviour
    {
        #region EVENT CREATION

        public static event Action RTriggerDownCallback;                              // Called the frame if the right trigger is pressed down
        public static event Action RTriggerPressedCallback;                           // Called any frame if the right trigger is pressed
        public static event Action RTriggerUpCallback;                                // Called the frame if the right trigger is released
        public static event Action RTriggerMovedCallback;                             // Called the frame if the right trigger is changed in value

        public static event Action RGripDownCallback;                                 // Called the frame if the right grip is pressed down
        public static event Action RGripPressedCallback;                              // Called any frame if the right grip is pressed
        public static event Action RGripUpCallback;                                   // Called the frame if the right grip is released

        public static event Action RTouchpadClickDownCallback;                        // Called the frame if the right touchpad is pressed down
        public static event Action RTouchpadClickPressedCallback;                     // Called the frame if the right touchpad is pressed
        public static event Action RTouchpadClickUpCallback;                          // Called the frame if the right touchpad is released

        public static event Action RTouchpadTouchDownCallback;                        // Called the frame if the right touchpad is pressed down
        public static event Action RTouchpadTouchPressedCallback;                     // Called the frame if the right touchpad is pressed
        public static event Action RTouchpadTouchUpCallback;                          // Called the frame if the right touchpad is released

        public static event Action RTouchpadMoved;                                    // Called the frame if the right touchpad axis is moved

        public static event Action LTriggerDownCallback;                              // Called the frame if the left trigger is pressed down
        public static event Action LTriggerPressedCallback;                           // Called any frame if the left trigger is pressed
        public static event Action LTriggerUpCallback;                                // Called the frame if the left trigger is released
        public static event Action LTriggerValueChangedCallback;                             // Called the frame if the right trigger is changed in value

        public static event Action LGripDownCallback;                                 // Called the frame if the left grip is pressed down
        public static event Action LGripPressedCallback;                              // Called any frame if the left grip is pressed
        public static event Action LGripUpCallback;                                   // Called the frame if the left grip is released

        public static event Action LTouchpadClickDownCallback;                        // Called the frame if the left touchpad is pressed down
        public static event Action LTouchpadClickPressedCallback;                     // Called the frame if the left touchpad is pressed
        public static event Action LTouchpadClickUpCallback;                          // Called the frame if the left touchpad is released

        public static event Action LTouchpadTouchDownCallback;                        // Called the frame if the right touchpad is pressed down
        public static event Action LTouchpadTouchPressedCallback;                     // Called the frame if the right touchpad is pressed
        public static event Action LTouchpadTouchUpCallback;                          // Called the frame if the right touchpad is released

        public static event Action LTouchpadMoved;                                    // Called the frame if the left touchpad axis is moved

        public static event Action LButtonOneDownCallback;                            // Called the frame if the A Button is pressed down
        public static event Action LButtonOnePressedCallback;                         // Called any frame if the A Button is pressed
        public static event Action LButtonOneUpCallback;                              // Called the frame if the A Button is released

        public static event Action LButtonTwoDownCallback;                            // Called the frame if the B Button is pressed down
        public static event Action LButtonTwoPressedCallback;                         // Called any frame if the b Button is pressed
        public static event Action LButtonTwoUpCallback;                              // Called the frame if the B Button is released

        public static event Action RButtonOneDownCallback;                            // Called the frame if the A Button is pressed down
        public static event Action RButtonOnePressedCallback;                         // Called any frame if the A Button is pressed
        public static event Action RButtonOneUpCallback;                              // Called the frame if the A Button is released

        public static event Action RButtonTwoDownCallback;                            // Called the frame if the B Button is pressed down
        public static event Action RButtonTwoPressedCallback;                         // Called any frame if the b Button is pressed
        public static event Action RButtonTwoUpCallback;                              // Called the frame if the B Button is released

        public static event Action MenuDownCallback;                                  // Called any frame if the menu button is pressed down
        public static event Action MenuPressedCallback;                               // Called any frame if the menu button is pressed down
        public static event Action MenuUpCallback;                                    // Called any frame if the menu button is released

        #endregion EVENT CREATION

        #region VARIABLES
       
        [Space(7)]
        [SerializeField] bool debugInput;

        public enum Inputs
        {            
            Trigger,
            Grip,
            TouchpadTouch,
            TouchpadClick,
            PrimaryButton
        }

        // Hand Objects
        private GameObject leftHand;
        private GameObject rightHand;

        // Left Hand
        private ButtonInput leftTrigger;
        private ButtonInput leftGrip;
        private ButtonInput leftTouchpadTouch;
        private ButtonInput leftTouchpadClick;
        private ButtonInput leftButtonOne;
        private ButtonInput leftButtonTwo;


        //Right Hand
        private ButtonInput rightTrigger;
        private ButtonInput rightGrip;
        private ButtonInput rightTouchpadTouch;
        private ButtonInput rightTouchpadClick;
        private ButtonInput rightButtonOne;
        private ButtonInput rightButtonTwo;

        #endregion VARIABLES

        private void Start()
        {
            InitializeHandObjects();
            InitializeButtonInput();
        }

        private void InitializeHandObjects()
        {
            leftHand = PlayerManager.Instance.leftHand;
            rightHand = PlayerManager.Instance.rightHand;
        }

        private void InitializeButtonInput()
        {
            // Left Hand
            leftTrigger = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.triggerButton);
            leftGrip = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.gripButton);
            leftTouchpadTouch = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.primary2DAxisTouch);
            leftTouchpadClick = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.primary2DAxisClick);
            leftButtonOne = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.primaryButton);
            leftButtonTwo = new ButtonInput(GetCurrentDevice(XRNode.LeftHand), CommonUsages.secondaryButton);

            // Right Hand
            rightTrigger = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.triggerButton);
            rightGrip = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.gripButton);
            rightTouchpadTouch = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.primary2DAxisTouch);
            rightTouchpadClick = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.primary2DAxisClick);
            rightButtonOne = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.primaryButton);
            rightButtonTwo = new ButtonInput(GetCurrentDevice(XRNode.RightHand), CommonUsages.secondaryButton);
        }

        private void Update()
        {
            
            SetDevicePosAndRot(XRNode.LeftHand, leftHand);
            SetDevicePosAndRot(XRNode.RightHand, rightHand);

          
            // Left Hand
            UpdateButton(leftTrigger, LTriggerDownCallback, LTriggerPressedCallback, LTriggerUpCallback);
            UpdateButton(leftGrip, LGripDownCallback, LGripPressedCallback, LGripUpCallback);
            UpdateButton(leftTouchpadTouch, LTouchpadTouchDownCallback, LTouchpadTouchPressedCallback, LTouchpadTouchUpCallback);
            UpdateButton(leftTouchpadClick, LTouchpadClickDownCallback, LTouchpadClickPressedCallback, LTouchpadClickUpCallback);
            UpdateButton(leftButtonOne, LButtonOneDownCallback, LButtonOnePressedCallback, LButtonOneUpCallback);
            UpdateButton(leftButtonTwo, LButtonTwoDownCallback, LButtonTwoPressedCallback, LButtonTwoUpCallback);

            // DEVIL CODE
            //UpdateButton(leftTrigger, LTriggerValueChangedCallback);
            
            // Right Hand
            UpdateButton(rightTrigger, RTriggerDownCallback, RTriggerPressedCallback, RTriggerUpCallback);
            UpdateButton(rightGrip, RGripDownCallback, RGripPressedCallback, RGripUpCallback);
            UpdateButton(rightTouchpadTouch, RTouchpadTouchDownCallback, RTouchpadTouchPressedCallback, RTouchpadTouchUpCallback);
            UpdateButton(rightTouchpadClick, RTouchpadClickDownCallback, RTouchpadClickPressedCallback, RTouchpadClickUpCallback);
            UpdateButton(rightButtonOne, RButtonOneDownCallback, RButtonOnePressedCallback, RButtonOneUpCallback);
            UpdateButton(rightButtonTwo, RButtonTwoDownCallback, RButtonTwoPressedCallback, RButtonTwoUpCallback);
          
        }
        
        // Check if a button has been pressed per frame
        private void UpdateButton(ButtonInput _buttonInput, Action _downEvent, Action _pressedEvent, Action _upEvent)
        {
            var device = _buttonInput.device;
            var button = _buttonInput.boolButton;

            if (device.TryGetFeatureValue(button, out bool boolValue))
            {               
                if (boolValue && !_buttonInput.boolValue)
                {
                    _buttonInput.boolValue = true;
                    _downEvent?.Invoke();

                    if (debugInput)
                        Debug.Log(device.name + " " + button.name + " down");
                }
                else if (!boolValue && _buttonInput.boolValue)
                {
                    _buttonInput.boolValue = false;
                    _upEvent?.Invoke();

                    if (debugInput)
                        Debug.Log(device.name + " " + button.name + " up");
                }
                else if (boolValue && _buttonInput.boolValue)
                {
                    _pressedEvent?.Invoke();

                    if (debugInput)
                        Debug.Log(device.name + " " + button.name + " pressed");
                }
            }
        }

        private void UpdateButton(ButtonInput _buttonInput, Action _valueChangedEvent)
        {
            var device = _buttonInput.device;
            var button = _buttonInput.floatButton;

            if (device.TryGetFeatureValue(button, out float floatValue))
            {
                if (_buttonInput.floatValue != floatValue)
                {
                    _buttonInput.floatValue = floatValue;
                    _valueChangedEvent?.Invoke();

                    if (debugInput)
                        Debug.Log(device.name + " " + button.name + " changed");
                }
            }

        }

        // Constantly updates the position and rotation of the XRNode
        private static void SetDevicePosAndRot(XRNode _trackedDevice, GameObject _hand)
        {
            InputDevice device = InputDevices.GetDeviceAtXRNode(_trackedDevice);
            if (device.isValid)
            {
                Vector3 position;
                Quaternion rotation;

                device.TryGetFeatureValue(CommonUsages.devicePosition, out position);
                device.TryGetFeatureValue(CommonUsages.deviceRotation, out rotation);

                _hand.transform.localRotation = rotation;
                _hand.transform.localPosition = position;

                return;
            }
        }

        public static InputDevice GetCurrentDevice(XRNode _node)
        {
            var device = new InputDevice();
            var devices = new List<InputDevice>();
            InputDevices.GetDevicesAtXRNode(_node,
                devices);

            string devicesFound = "Devices found: ";
            for (int i = 0; i < devices.Count; i++)
            {
                devicesFound += (devices[i].name);
                Debug.Log(devicesFound);
            }


            if (devices.Count == 1)
            {
                for (int i = 0; i < devices.Count; i++)
                {
                    if (devices[i].isValid)
                    {
                        device = devices[i];
                    }
                }
                
                // Use for debugging
                //Debug.Log($"Device name '{device.name}' with role '{device.characteristics.ToString()}'");
            }
            else if (devices.Count > 1)
            {
                // Use for debugging
                //Debug.Log($"Found more than one '{device.characteristics.ToString()}'!");
                device = devices[0];
            }

            return device;
        }
    }
}