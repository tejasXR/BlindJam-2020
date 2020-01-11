using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INPUTACTIONS
{
    public class ShowCylinder : MonoBehaviour
    {
        public GameObject cylinder;

        private void Start()
        {
            cylinder.SetActive(false);
        }

        public void Show()
        {
            cylinder.SetActive(true);
        }

        public void Hide()
        {
            cylinder.SetActive(false);
        }
    }
}


