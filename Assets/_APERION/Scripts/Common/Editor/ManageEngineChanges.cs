using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace APERION
{
    public class ManageEngineChanges : EditorWindow
    {
        [SerializeField] private string aperionEnginePath = "";

        [MenuItem("APERION/Manage Engine Changes")]
        private static void ManageChanges()
        {
            GetWindow<ManageEngineChanges>();
        }

        protected void OnEnable()
        {
            // Here we retrieve the data if it exists or we save the default field initialisers we set above
            var data = EditorPrefs.GetString("ManageEngineChanges", JsonUtility.ToJson(this, false));
            // Then we apply them to this window
            JsonUtility.FromJsonOverwrite(data, this);
        }

        protected void OnDisable()
        {
            // We get the Json data
            var data = JsonUtility.ToJson(this, false);
            // And we save it
            EditorPrefs.SetString("ManageXRCOREChanges", data);
        }

        private void OnGUI()
        {
            aperionEnginePath = EditorGUILayout.TextField("Aperion Engine Path: ", aperionEnginePath);

            GUILayout.Label("Make sure to link the Aperion Engine path to the '_APERION' folder within your XRCORE repo");

            if (GUILayout.Button("Browse for Aperion Engine Repo"))
            {
                aperionEnginePath = EditorUtility.OpenFolderPanel("Select Aperion Engine Repo Location", "", "");

                Debug.Log("Hey there, you've set your Aperion Engine repo folder to be: " + aperionEnginePath);
            }

            if (GUILayout.Button("Push Engine Changes"))
            {
                if (Directory.Exists(aperionEnginePath))
                {
                    FileUtil.ReplaceDirectory("Assets/_APERION", aperionEnginePath);

                    Debug.Log("Hey there, the files in your _APERION folder have been sent over to your Aperion Engine repo!");
                }
            }

            if (GUILayout.Button("Pull Engine Changes"))
            {
                if (Directory.Exists(aperionEnginePath))
                {
                    FileUtil.ReplaceDirectory(aperionEnginePath, "Assets/_APERION");

                    AssetDatabase.Refresh();

                    Debug.Log("Hey there, the files in your Aperion Engine repo have been pulled into your _APERION folder!");
                }
            }
        }
    }
}


