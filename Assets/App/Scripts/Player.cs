using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace APERION.BlindJam
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [SerializeField] AudioSource playerDeathAudio;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

        }

        public static void GetPlayerAlive()
        {

        }

       public void PlayerDies()
       {
            StartCoroutine(PlayerDeathSequence());
       }

        private IEnumerator PlayerDeathSequence()
        {
            playerDeathAudio.Play();

            yield return new WaitForSeconds(3F);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}


