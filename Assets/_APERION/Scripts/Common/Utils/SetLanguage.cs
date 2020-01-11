using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using APERION;

namespace APERION
{
    public class SetLanguage : MonoBehaviour
    {
        public void SetLocalizatedLanguage(string localizedFile)
        {
            LocalizationManager.Instance.SetLanguage(localizedFile);
        }
    }
}