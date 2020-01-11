using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace APERION
{
    public class AnalyticsManager : MonoBehaviour
    {
        public static void CallCustomEvent(string _customEventName)
        {
            Analytics.CustomEvent(_customEventName);
        }

        //public static void CallCustomEvent(string _customEventName, )
    }
}


