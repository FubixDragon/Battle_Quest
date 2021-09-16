using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Core
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] string[] DisplayText;
        [SerializeField] GameObject DialogBox = null;
        [SerializeField] bool removeAfterUse = false;
        [SerializeField] bool repeatOnNextReload = true;

        [SerializeField] bool ChangeScene = false;
        [SerializeField] int TargetScene = 0;

        public bool hasYesNoQuestion = false;
        GameObject Player;


        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }



        private void OnTriggerEnter(Collider other)
        {
         
            if(other.name == "Player")
            {
                DialogBox.SetActive(true);

                // give Dialog Box the text to display
                DialogBox.GetComponent<dialogHolder>().onRecieveText(DisplayText, ChangeScene, TargetScene, hasYesNoQuestion);
                Player.GetComponent<PlayerAreaExplorererController>().Cancel();
                Player.GetComponent<PlayerAreaExplorererController>().controlsEnabled = false;

                // remove trigger box after triggered if you don't want
                // dialog to reappear when you cross again later.
                if (removeAfterUse) Destroy(this.gameObject);
            }
            
        }

       
    }
}

