using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fubix.Core;

namespace Fubix.SceneManagment
{
    public class FaderMyWay : MonoBehaviour
    {
        private float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;

        GameObject player;
        public bool fadeOut = false;
        public bool fadeIn = false;
         
        Image faderScreen;
        float currentFadeTime = 0;


        private void Start()
        {
            faderScreen = this.gameObject.GetComponent<Image>();
            player = GameObject.FindGameObjectWithTag("Player");
            startFadeIn(fadeInTime);
        }


        private void Update()
        {
            if (fadeOut) fadingOut();
            if (fadeIn) fadingIn();

            if (Input.GetKeyDown(KeyCode.A))
            {
                fadeOut = true;

            }
        }

        public void fadingIn()
        {
            currentFadeTime -= Time.deltaTime / fadeInTime;

            faderScreen.color = new Color(255, 255, 255, currentFadeTime);

            // stop timer
            if (currentFadeTime <= 0)
            {
             //   player.GetComponent<PlayerAreaExplorererController>().StartNavMeshAgent();
                currentFadeTime = 0;
                fadeIn = false;
            }
        }

        public void fadingOut()
        {
            currentFadeTime += Time.deltaTime / fadeOutTime;

            faderScreen.color = new Color(255, 255, 255, currentFadeTime);

            // stop timer.. I feel like >= var should be 1
            if (currentFadeTime >= fadeOutTime) fadeOut = false;
            
            
        }
      

        public void startFadeIn(float fadeTime)
        {
           // player.GetComponent<PlayerAreaExplorererController>().Cancel();
            fadeIn = true;
            fadeInTime = fadeTime;
            currentFadeTime = fadeInTime;
        }

        public void startFadeOut(float fadeTime)
        {
           // player.GetComponent<PlayerAreaExplorererController>().Cancel();
            fadeOut = true;
            fadeOutTime = fadeTime;
            currentFadeTime = 0;
        }
    }

}
