using UnityEngine;

namespace APERION
{
    public static class VersionHelper
    {
        private static string ACTIVITY_NAME = "com.unity3d.player.UnityPlayer";
        private static string CONTEXT = "currentActivity";
        private static string VERSION_HELPER_CLASS = "com.oculus.versionhelper.VersionHelper";

        private static AndroidJavaObject versionHelper = null;

        public static string ExpansionFileLocation
        {
            get
            {
#if UNITY_ANDROID
                if (versionHelper == null)
                {
                    AndroidJavaObject context = new AndroidJavaClass(ACTIVITY_NAME).GetStatic<AndroidJavaObject>(CONTEXT);
                    versionHelper = new AndroidJavaObject(VERSION_HELPER_CLASS, context);
                }
                return versionHelper.Call<string>("GetExpansionFilePath");
#else
              return "";
#endif
            }
        }

        public static int GetVersionCode()
        {
            AndroidJavaClass up = new AndroidJavaClass(ACTIVITY_NAME);

            var ca = up.GetStatic<AndroidJavaObject>(CONTEXT);

            AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

            var pInfo = packageManager.Call<AndroidJavaObject>("getPackageInfo", Application.identifier, 0);

            Debug.Log("versionCode:" + pInfo.Get<int>("versionCode"));

            return pInfo.Get<int>("versionCode");
        }
    }
}

