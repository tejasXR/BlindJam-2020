using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace APERION
{
    // A script used to fade in and out between scenes

    public class FadeTransition : MonoBehaviour
    {
        public static FadeTransition Instance;

        public Color sceneTransitionColor;
        public Color teleportTransitionColor;

        [Tooltip("Delay before the fade starts")]
        public float fadePredelay;

        [Tooltip("Length of the fade when transitioning between scenes")]
        public float sceneFadeLength;

        [Tooltip("Length of the fade when transitioning between teleport points")]
        public float teleportFadeLength;


        [HideInInspector]
        public bool fadeComplete;

        public enum FadeType
        {
            FadeIn,
            FadeOut
        }

        private Image fadeImage;
        private bool fadeIn;
        private bool fadeOut;
        private float fadePredelayCounter;
        private float fadeLength;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            fadeImage = GetComponentInChildren<Image>();
            LoadScene(FadeType.FadeIn);
        }

        private void Update()
        {
            if (fadeIn && !fadeOut && !fadeComplete)
                FadeIn();

            if (fadeOut && !fadeIn && !fadeComplete)
                FadeOut();
        }

        public void LoadScene(FadeType fadeType)
        {
            fadeImage.color = sceneTransitionColor;

            fadeLength = sceneFadeLength;

            fadeComplete = false;
            fadePredelayCounter = fadePredelay;

            ResetFadeValues();

            switch (fadeType)
            {
                case FadeType.FadeIn:                   
                    fadeIn = true;
                    break;

                case FadeType.FadeOut:
                    fadeOut = true;
                    break;
            }
        }

        public void TeleportTransition(FadeType fadeType)
        {
            Color tempColor = teleportTransitionColor;

            fadeLength = teleportFadeLength;

            fadeComplete = false;

            fadePredelayCounter = fadePredelay;

            ResetFadeValues();

            switch (fadeType)
            {
                case FadeType.FadeIn:
                    tempColor.a = 1;
                    fadeIn = true;
                    break;

                case FadeType.FadeOut:
                    tempColor.a = 0;
                    fadeOut = true;
                    break;
            }

            fadeImage.color = tempColor;
        }
            
        private void FadeIn()
        {
            FadePreDelay();

            if (fadePredelayCounter <= 0)
            {
                if (fadeImage.color.a >= 0)
                {
                    Color tempColor = fadeImage.color;                    

                    tempColor.a -= Time.deltaTime * (1 / fadeLength);

                    fadeImage.color = tempColor;

                    if (fadeImage.color.a <= 0)
                    {
                        fadeComplete = true;
                        ResetFadeValues();
                    }
                }                
            }           
        }

        private void FadeOut()
        {
            FadePreDelay();

            if (fadePredelayCounter <= 0)
            {
                if (fadeImage.color.a <= 1)
                {
                    Color tempColor = fadeImage.color;

                    tempColor.a += Time.deltaTime * (1 / fadeLength);

                    fadeImage.color = tempColor;

                    if (fadeImage.color.a >= 1)
                    {
                        fadeComplete = true;
                        ResetFadeValues();
                    }
                }
            }           
        }

        private void FadePreDelay()
        {
            if (fadePredelayCounter > 0)
            {
                fadePredelayCounter -= Time.deltaTime;
            }
        }

        private void ResetFadeValues()
        {
            fadeIn = false;
            fadeOut = false;
        }
    }
}


