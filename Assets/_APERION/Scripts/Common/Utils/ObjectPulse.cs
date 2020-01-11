using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public class ObjectPulse : MonoBehaviour
    {
        public Renderer[] rendsToFlash;
        public Color pulseEmissionColor;
        public Color dimColor;
        public float pulseSpeed;

        private Color[] originalEmissionColors;
        private Color[] originalMainColors;
        private bool pulseToOriginal;
        private float pulseTimer;
        private bool isOriginalColor;
        private bool shouldPulse;

        void Start()
        {
            GetOriginalColors();
            EnableKeyword();
            ResetPulseSpeed();

            isOriginalColor = true;
        }

        void Update()
        {
            if (!shouldPulse)
            {
                if (!isOriginalColor)
                {
                    SetToOriginalColor();
                }

                return;
            }
                

            PulseMaterials();
            PulseTimer();
        }

        public void PulseOn()
        {
            shouldPulse = true;
        }

        public void PulseOff()
        {
            shouldPulse = false;
        }

        public void DimColor()
        {
            for (int i = 0; i < rendsToFlash.Length; i++)
            {
                rendsToFlash[i].material.SetColor("_Color", dimColor);
            }
        }

        public void NormalColor()
        {
            for (int i = 0; i < rendsToFlash.Length; i++)
            {
                rendsToFlash[i].material.SetColor("_Color", originalMainColors[i]);
            }
        }

        private void PulseMaterials()
        {          
            if (!pulseToOriginal)
            {
                for (int i = 0; i < rendsToFlash.Length; i++)
                {
                    rendsToFlash[i].material.SetColor("_EmissionColor", Color.Lerp(rendsToFlash[i].material.GetColor("_EmissionColor"), pulseEmissionColor, Time.deltaTime * pulseSpeed));
                }                
            }
            else
            {
                for (int i = 0; i < rendsToFlash.Length; i++)
                {
                    rendsToFlash[i].material.SetColor("_EmissionColor", Color.Lerp(rendsToFlash[i].material.GetColor("_EmissionColor"), originalEmissionColors[i], Time.deltaTime * pulseSpeed));
                }                
            }

            isOriginalColor = false;
        }

        private void PulseTimer()
        {
            pulseTimer -= Time.deltaTime;

            if (pulseTimer <= 0)
            {
                ResetPulseSpeed();
                pulseToOriginal = !pulseToOriginal;
            }                
        }

        private void EnableKeyword()
        {
            foreach (var r in rendsToFlash)
            {
                r.material.EnableKeyword("_EMISSION");
            }
        }

        private void SetToOriginalColor()
        {
            //if (GetComponent<ObjectFlash>() != null)
            //{
            //    GetComponent<ObjectFlash>().Flash();
            //}                
            //else
            {
                for (int i = 0; i < rendsToFlash.Length; i++)
                {
                    rendsToFlash[i].material.SetColor("_EmissionColor", originalEmissionColors[i]);
                    rendsToFlash[i].material.SetColor("_Color", originalMainColors[i]);

                }
            }

            isOriginalColor = true;           
        }

        private void GetOriginalColors()
        {
            originalEmissionColors = new Color[rendsToFlash.Length];

            for (int i = 0; i < originalEmissionColors.Length; i++)
            {
                originalEmissionColors[i] = rendsToFlash[i].material.GetColor("_EmissionColor");
            }

            originalMainColors = new Color[rendsToFlash.Length];

            for (int i = 0; i < originalMainColors.Length; i++)
            {
                originalMainColors[i] = rendsToFlash[i].material.GetColor("_Color");
            }
        }

        private void ResetPulseSpeed()
        {
            pulseTimer = 1 / pulseSpeed;

        }
    }
}


