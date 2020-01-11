using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace APERION.VR
{
    // A script which configure rendering settings for the Oculus Go

    public class VRRenderingManager : MonoBehaviour
    {
        public float renderingScale;

        private void Start()
        {           
            SetRenderScale();
        }


        private void SetRenderScale()
        {
            XRSettings.eyeTextureResolutionScale = renderingScale;
        }
        

        /*
        public float renderingScale;

        public enum FFRSetting
        {
            Off,
            Low,
            Medium,
            High,
            HighTop
        }

        [Tooltip("Fixed Foveated Rendering Setting")]
        public FFRSetting fixedFoveatedRendering;

        public enum DisplayRefreshRate
        {
            Standard,
            Smooth
        }

        public DisplayRefreshRate displayRefreshRate;

        private void Start()
        {
            SetRefreshRate();
            SetFRR();
            SetRenderScale();
        }

        // Sets refresh rate
        private void SetRefreshRate()
        {
            switch (displayRefreshRate)
            {
                case DisplayRefreshRate.Standard:
                    OVRManager.display.displayFrequency = 60.0f;
                    break;

                case DisplayRefreshRate.Smooth:
                    OVRManager.display.displayFrequency = 72.0f;
                    break;
            }
        }

        // Sets FFR
        private void SetFRR()
        {
            switch (fixedFoveatedRendering)
            {
                case FFRSetting.Off:
                    OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Off;
                    break;

                case FFRSetting.Low:
                    OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Low;
                    break;

                case FFRSetting.Medium:
                    OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.Medium;
                    break;

                case FFRSetting.High:
                    OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.High;
                    break;

                case FFRSetting.HighTop:
                    OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.HighTop;
                    break;
            }
        }

        // Sets rendering scale
        private void SetRenderScale()
        {
            XRSettings.eyeTextureResolutionScale = renderingScale;
        }
        */
    }
}


