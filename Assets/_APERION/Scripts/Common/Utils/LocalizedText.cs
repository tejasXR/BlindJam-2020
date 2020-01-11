using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace APERION
{
    public class LocalizedText : MonoBehaviour
    {
        public string key;

        private string textLocalized;

        void Start()
        {
            // Checks if there is a Localization =Manager Insatnce, then finds the key-value pair to localize text
            if (LocalizationManager.Instance != null)
            {
                textLocalized = LocalizationManager.Instance.GetLocalizedValue(key);

                if (GetComponent<Text>() != null)
                {
                    Text text = GetComponent<Text>();
                    text.text = textLocalized;
                }

                if (GetComponent<TextMeshProUGUI>() != null)
                {
                    TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
                    text.text = textLocalized;
                }

                if (GetComponent<TextMeshPro>() != null)
                {
                    TextMeshPro text = GetComponent<TextMeshPro>();
                    text.text = textLocalized;
                }
            }           
        }
    }
}


