using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace APERION.VR.INTERACTIVE
{
    public class Pagination : MonoBehaviour
    {

        public InteractiveItem nextButton;
        public InteractiveItem backButton;

        [Space(7)]
        public GameObject[] prompts;

        private int currentPrompt;

        private void OnEnable()
        {
            nextButton.ItemUsedCallback += NextPrompt;
            backButton.ItemUsedCallback += PreviosPrompt;
        }

        private void OnDisable()
        {
            nextButton.ItemUsedCallback -= NextPrompt;
            backButton.ItemUsedCallback -= PreviosPrompt;
        }

        void Start()
        {
            currentPrompt = 0;
            backButton.gameObject.SetActive(false);

            ShowPrompt(0);
        }

        public void NextPrompt()
        {            
            currentPrompt++;

            if (currentPrompt == prompts.Length)
            {
                nextButton.gameObject.SetActive(false);
            }

            backButton.gameObject.SetActive(true);
            
            ShowPrompt(currentPrompt);
        }

        public void PreviosPrompt()
        {           
            currentPrompt--;

            if (currentPrompt == 0)
            {
                backButton.gameObject.SetActive(false);
            }

            nextButton.gameObject.SetActive(true);

            ShowPrompt(currentPrompt);
        }

        private void ShowPrompt(int _promptIndex)
        {
            for (int i = 0; i < prompts.Length; i++)
            {
                if (i != _promptIndex)
                {
                    prompts[i].SetActive(false);
                }
                else
                {
                    prompts[i].SetActive(true);
                }
            }           
        }
    }
}

