using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION
{
    // Loads predetermined assets on start

    public class LoadAssets : MonoBehaviour
    {
        public static LoadAssets Instance;

        [Tooltip("Insert names of AssetBundles you would like to load")]
        public List<string> bundleNames = new List<string>();

        private List<AssetBundle> assetBundles = new List<AssetBundle>();

        private string assetURL = "";

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            StartCoroutine(Load());          
        }

        // Loads bundles from given directories
        private IEnumerator Load()
        {
            for (int i = 0; i < bundleNames.Count; i++)
            {

#if UNITY_ANDROID
                assetURL = "/sdcard/Android/obb/" + Application.identifier
                    + "/" + bundleNames[i];
#endif

#if UNITY_EDITOR
                assetURL = "D:/Tejas/XPO Projects/Interactive 360/xea-vr-warehouse-marketing/AssetBundles/Android/orc/" + bundleNames[i];
#endif
                var listOfBundles = AssetBundle.GetAllLoadedAssetBundles();
              
                foreach (var l in listOfBundles)
                {
                    if (bundleNames[i] == l.name)
                    {
                        Debug.Log("Currently loaded asset bundle :" + bundleNames[i]);

                        yield break;
                    }                                
                }

                AssetBundleCreateRequest bundle = AssetBundle.LoadFromFileAsync(assetURL);

                Debug.Log("Loading asset bundle: " + bundle.assetBundle.name);

                AssetBundle myLoadedAssetBundle = bundle.assetBundle;
                if (myLoadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    yield break;
                }
                else
                {
                    assetBundles.Add(myLoadedAssetBundle);
                }
            }
        }

        public AssetBundle GetAssetBundle(string bundleName)
        {
            for (int i = 0; i < bundleNames.Count; i++)
            {
                if (bundleNames[i] == bundleName)
                {
                    return assetBundles[i];
                }
            }

            return null;
        }
    }
}


