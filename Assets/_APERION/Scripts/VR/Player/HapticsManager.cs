using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace APERION
{
    public class HapticsManager : MonoBehaviour
    {
        public static HapticsManager Instance;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void HapticPulse(float _length, float _strength, InputDevice _device)
        {
            //StartCoroutine(HapticPulseRoutine(_length, _strength, _device));

            HapticCapabilities haptics;
            if (_device.TryGetHapticCapabilities(out haptics))
            {
                uint channel = 0;
                _device.SendHapticImpulse(channel, _strength, _length);
            }
        }      

        /*
        private IEnumerator HapticPulseRoutine(float _length, float _strength, InputDevice _device)
        {
            HapticCapabilities haptics;
            if (_device.TryGetHapticCapabilities(out haptics))
            {
                uint channel = 0;
                float amplitude = 0.5f;
                float duration = 1.0f;
                device.SendHapticImpulse(channel, amplitude, duration);
            }


            OVRInput.SetControllerVibration(1, _strength, _controller);

            yield return new WaitForSeconds(_length);

            OVRInput.SetControllerVibration(0, 0, _controller);


        }
        */
    }
}


