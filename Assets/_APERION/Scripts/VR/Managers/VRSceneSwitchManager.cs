using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace APERION.VR
{
    // A script in charge of switching scenes

    public class VRSceneSwitchManager : MonoBehaviour
    {

        #region EVENTS
        public static event Action SceneLoadedCallback;

        protected virtual void OnSceneLoaded()
        {
            SceneLoadedCallback?.Invoke();
        }
        #endregion

        #region VARIABLES

        public static VRSceneSwitchManager Instance;

        public bool useFade;

        [HideInInspector]
        public bool changingScene;

        [HideInInspector]
        public bool levelLoaded;

        private FadeTransition fadeTransition;
        private AsyncOperation asyncLoadLevel;

        #endregion

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

            if (FindObjectOfType<FadeTransition>() == null)
            {
                Debug.LogError("There is no FadeTransition object in the scene!");
            }
            else
            {
                fadeTransition = FindObjectOfType<FadeTransition>();
            }
        }

        public void SwitchScene(int _sceneToLoad)
        {
            if (!changingScene)
            {
                if (fadeTransition == null)
                {
                    StartCoroutine(SceneTransitionNoFade(_sceneToLoad));
                }
                else
                {
                    StartCoroutine(SceneTransitionWithFade(_sceneToLoad));
                }
            }
        }
        
        private IEnumerator SceneTransitionWithFade(int _sceneToLoad)
        {
            changingScene = true;
            levelLoaded = false;

            // Fade Out
            fadeTransition.LoadScene(FadeTransition.FadeType.FadeOut);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            // Loads Scene
            asyncLoadLevel = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadLevel.isDone);

            Debug.Log("Next scene loaded");

            OnSceneLoaded();

            levelLoaded = true;

            var videoPlayer = FindObjectOfType<VideoPlayer>();
            if (videoPlayer != null)
            {
                yield return new WaitForSeconds(2F);
                //yield return new WaitUntil(() => videoPlayer.isPrepared);
            }
            else
                Debug.Log("There's no video in the scene");

            Debug.Log("Video buffer time ended");            

            // Fades In
            fadeTransition.LoadScene(FadeTransition.FadeType.FadeIn);
            yield return new WaitUntil(() => fadeTransition.fadeComplete);

            changingScene = false;
        }

        private IEnumerator SceneTransitionNoFade(int _sceneToLoad)
        {
            changingScene = true;
            levelLoaded = false;

            // Loads Scene
            asyncLoadLevel = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Single);
            yield return new WaitUntil(() => asyncLoadLevel.isDone);

            Debug.Log("Next scene loaded");

            OnSceneLoaded();

            levelLoaded = true;

            var videoPlayer = FindObjectOfType<VideoPlayer>();
            if (videoPlayer != null)
            {
                yield return new WaitForSeconds(2F);
                //yield return new WaitUntil(() => videoPlayer.isPrepared);
            }
            else
                Debug.Log("There's no video in the scene");

            Debug.Log("Video buffer time ended");

            changingScene = false;
        }
    }
}