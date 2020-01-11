using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace APERION
{
    public class LocalizedTextEditor : EditorWindow
    {

        public LocalizationData m_localizationData;

        [MenuItem("XRCORE/Localized Text Editor")]
        static void Init()
        {
            EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
        }

        private void OnGUI()
        {
            if (m_localizationData != null)
            {
                SerializedObject serializedObject = new SerializedObject(this);
                SerializedProperty serializedProperty = serializedObject.FindProperty("m_localizationData");
                EditorGUILayout.PropertyField(serializedProperty, true);
                serializedObject.ApplyModifiedProperties();

                if (GUILayout.Button("Save data"))
                {
                    SaveGameData();
                }
            }

            if (GUILayout.Button("Load data"))
            {
                LoadGameData();
            }

            if (GUILayout.Button("Create new data"))
            {
                CreateNewData();
            }
        }

        private void LoadGameData()
        {
            string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath, "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);

                m_localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            }
        }

        private void SaveGameData()
        {
            string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = JsonUtility.ToJson(m_localizationData);
                File.WriteAllText(filePath, dataAsJson);
            }
        }

        private void CreateNewData()
        {
            m_localizationData = new LocalizationData();
        }

    }
}


