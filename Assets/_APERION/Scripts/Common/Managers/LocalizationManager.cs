using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

namespace APERION
{
    public class LocalizationManager : MonoBehaviour
    {        
        public static LocalizationManager Instance;

        public enum StartingLanguage
        {
            None,
            English,
            French,
            Spanish
        }

        public StartingLanguage startingLanguage;

        public enum InterantionalRegion
        {
            NorthAmerica,
            European
        }

        public InterantionalRegion internationalRegion;

        [Space(7)]
        public string englishLanguageFile;
        public string frenchLanguageFile;
        public string spanishLanguageFile;


        private Dictionary<string, string> localizedText;
        private bool isReady = false;
        private string missingTextString = "Localized text not found";
        private string streamingAssetPath;
    
        void Awake()
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
            SelectStartingLanguage();
        }

        private void SelectStartingLanguage()
        {
            switch (startingLanguage)
            {
                case StartingLanguage.None:
                    return;

                case StartingLanguage.English:
                    SetLanguage(englishLanguageFile);
                    break;

                case StartingLanguage.French:
                    SetLanguage(frenchLanguageFile);
                    break;

                case StartingLanguage.Spanish:
                    SetLanguage(spanishLanguageFile);
                    break;
            }
        }

        private void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();

            streamingAssetPath = Application.streamingAssetsPath;
            string filePath = Path.Combine(streamingAssetPath + "/", fileName);

            Debug.Log("Searching through filePath: " + filePath);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                    Debug.Log(loadedData.items[i].key);
                }

                Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
            }
            else
            {
                Debug.LogError("Cannot find file!");
            }

            isReady = true;
        }

        private IEnumerator LoadLocalizedTextOnAndroid(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath;
            filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
            string dataAsJson;
            if (filePath.Contains("://") || filePath.Contains(":///"))
            {
                //debugText.text += System.Environment.NewLine + filePath;
                Debug.Log("UNITY:" + System.Environment.NewLine + filePath);
                UnityWebRequest www = UnityWebRequest.Get(filePath);
                yield return www.SendWebRequest();
                dataAsJson = www.downloadHandler.text;
            }
            else
            {
                dataAsJson = File.ReadAllText(filePath);
            }
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                Debug.Log("KEYS:" + loadedData.items[i].key);
            }

            isReady = true;
        }

        public void SetLanguage(string fileName)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                LoadLocalizedText(fileName);
            else if (Application.platform == RuntimePlatform.OSXEditor)
                LoadLocalizedText(fileName);
            else if (Application.platform == RuntimePlatform.Android)
                StartCoroutine(LoadLocalizedTextOnAndroid(fileName));
        }
       
        public string GetLocalizedValue(string key)
        {
            if (!isReady)
            {
                SelectStartingLanguage();
            }

            string result = missingTextString;

            try
            {
                if (localizedText.ContainsKey(key))
                {
                    result = localizedText[key];
                }

                return result;
            }
            catch (System.Exception)
            {
                Debug.LogError("Can't find key for text");
                throw;
            }
        }

        public bool GetIsReady()
        {
            return isReady;
        }

    }   
}


