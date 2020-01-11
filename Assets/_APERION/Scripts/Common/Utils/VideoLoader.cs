using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

namespace APERION
{
    // A script that loads a video from an AssetBundle

    [RequireComponent(typeof(VideoPlayer))]
    public class VideoLoader : MonoBehaviour
    {        
        public string videoName = "";
        public string bundleName = "";

        private string assetURL = "";
        private VideoPlayer videoPlayer = null;
        private AssetBundle assetBundle = null;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();

            StartCoroutine(LoadVideoFromBundle());
        }
        
        private IEnumerator LoadVideoFromBundle()
        {
            if (videoName != "" && bundleName != "")
            {
                AssetBundleRequest request = LoadAssets.Instance.GetAssetBundle(bundleName).LoadAssetAsync<VideoClip>(videoName);
                yield return request;

                VideoClip assetClip = request.asset as VideoClip;

                videoPlayer.clip = assetClip;
                videoPlayer.Play();
            }

            yield return null;
            
        }
    }
}


