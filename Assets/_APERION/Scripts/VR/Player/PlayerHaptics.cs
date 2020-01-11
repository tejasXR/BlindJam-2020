using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace APERION.VR
{
    public class PlayerHaptics : MonoBehaviour
    {
        public static void SendHaptics(XRNode _node, float _amplitude, float _duration)
        {
            var device = PlayerInput.GetCurrentDevice(_node);

            HapticCapabilities capabilities;

            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = _amplitude;
                    float duration = _duration;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }

        public static void StopHaptics(XRNode _node)
        {
            var device = PlayerInput.GetCurrentDevice(_node);

            device.StopHaptics();
        }
    }
}

