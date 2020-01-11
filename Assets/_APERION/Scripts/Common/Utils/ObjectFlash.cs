using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    public class ObjectFlash : MonoBehaviour
    {

        public Renderer[] rendsToFlash;

        public Color flashColor;
        private Color[] originalColors;
        private bool isResetting;

        void Start()
        {
            GetOriginalColors();
            EnableKeyword();
        }

        void Update()
        {
            if (isResetting)
            {
                ResetColor();
            }
        }
        public void Flash()
        {
            foreach (var r in rendsToFlash)
            {
                r.material.SetColor("_EmissionColor", flashColor);
            }

            StartCoroutine(ResetColorTimer(5F));
        }

        private void EnableKeyword()
        {
            foreach (var r in rendsToFlash)
            {
                r.material.EnableKeyword("_EMISSION");
            }
        }

        private void GetOriginalColors()
        {
            originalColors = new Color[rendsToFlash.Length];

            for (int i = 0; i < originalColors.Length; i++)
            {
                originalColors[i] = rendsToFlash[i].material.GetColor("_EmissionColor");
            }
        }
       
        private void ResetColor()
        {
            for (int i = 0; i < originalColors.Length; i++)
            {
                var current = rendsToFlash[i].material.GetColor("_EmissionColor");
                rendsToFlash[i].material.SetColor("_EmissionColor", Color.Lerp(current, originalColors[i], Time.deltaTime * 2.5F));
            }
        }

        private IEnumerator ResetColorTimer(float timer)
        {
            isResetting = true;

            yield return new WaitForSeconds(timer);
           
            isResetting = false;
        }
    }
}


