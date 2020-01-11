using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


namespace APERION
{
    // A script that reset the current view of the user

    public class ResetPlayerView : MonoBehaviour
    {
        private void Start()
        {
            InputTracking.Recenter();
        }
    }
}


