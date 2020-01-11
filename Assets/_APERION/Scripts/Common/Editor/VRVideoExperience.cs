using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;

namespace APERION.VR.TEMPLATES
{
    public class VRVideoExperience: EditorWindow
    {
        //[SerializeField] public VideoClip videoClip;
        [SerializeField] public List<VideoClip> videoClip;

        public VideoClip clip;

        [MenuItem("XRCORE/Templates/360 Video Experience")]
        private static void Create360Experience()
        {
            GetWindow<VRVideoExperience>("Create a 360 Video Experience");

        }

        private void OnGUI()
        {
            EditorGUILayout.ObjectField(clip, typeof(VideoClip), true);
            EditorGUILayout.LabelField("sample label");
            //videoClip = (VideoClip)EditorGUILayout.ObjectField("Clip", videoClip, typeof(VideoClip), false);
            //videoClip = (VideoClip)EditorGUILayout.Foldout(true, new GUIContent("Clips"));

            if (GUILayout.Button("Replace"))
            {

            }

        }
    }
}


