using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Fubix.SceneManagment;
using Fubix.Saving;

namespace Fubix.Core
{
    public class dialogHolder : MonoBehaviour
    {
        [SerializeField] Text dialogText;
        [SerializeField] Button YesButton;
        [SerializeField] Button NoButton;

        public string[] recievedText;
        private bool isChangeScene = false;
        private bool hasYesNoQuestion = false;
        private int sceneToGoTo = 0;

        GameObject Player;
        GameObject savingObject;

        int pageToView = 0;

        private void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            savingObject = GameObject.FindGameObjectWithTag("Saving");
        }


        public void yesDialogAnswer()
        {

            pageToView = 0;
            this.gameObject.SetActive(false);
            YesButton.gameObject.SetActive(false);
            NoButton.gameObject.SetActive(false);

            Player.GetComponent<PlayerAreaExplorererController>().StartNavMeshAgent();

            // change scene if set to true.
            if (isChangeScene)
            {
                savingObject.GetComponent<SavingWrapper>().Save();
                GetComponent<LoadNewScene>().ChangeScene(sceneToGoTo);
            }


        }

        public void noDialogAnswer()
        {
            pageToView = 0;
            this.gameObject.SetActive(false);
            YesButton.gameObject.SetActive(false);
            NoButton.gameObject.SetActive(false);

            Player.GetComponent<PlayerAreaExplorererController>().StartNavMeshAgent();
        }
        public void nextDialogText()
        {
            pageToView += 1;
            if (recievedText.Length == pageToView && hasYesNoQuestion)
            {
                YesButton.gameObject.SetActive(true);
                NoButton.gameObject.SetActive(true);
            }
            else if (recievedText.Length == pageToView)
            {
                pageToView = 0;
                this.gameObject.SetActive(false);

                Player.GetComponent<PlayerAreaExplorererController>().StartNavMeshAgent();

                // change scene if set to true.
                if (isChangeScene)
                {
                    savingObject.GetComponent<SavingWrapper>().Save();
                    GetComponent<LoadNewScene>().ChangeScene(sceneToGoTo);
                }

            }
            else
            {
                dialogText.text = recievedText[pageToView];

            }

        }

        public void onRecieveText(string[] text, bool ChangeScene, int targetScene, bool yesOrNoQuestion)
        {
            recievedText = text;
            dialogText.text = recievedText[0];

            //set if and what scene to change to after dialog is complete.
            sceneToGoTo = targetScene;
            isChangeScene = ChangeScene;
            hasYesNoQuestion = yesOrNoQuestion;


        }


    }
}
