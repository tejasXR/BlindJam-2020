using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace APERION.VR.EDITOR
{
    /// <summary>
    /// Custom editor function that creates default Hand Actions for Hand.cs within the Inspector
    /// </summary>
    [CustomEditor(typeof(Hand))]
    public class HandEditor : Editor
    {
        private bool createdHandActions;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Hand handScript = (Hand)target;

            if (!createdHandActions)
            {
                if (GUILayout.Button("Create Basic Hand Actions"))
                {
                    handScript.CreateBasicHandActions();

                    createdHandActions = true;
                }
            }
        }
    }
}
