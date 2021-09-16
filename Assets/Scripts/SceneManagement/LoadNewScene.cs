using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fubix.SceneManagment
{
    public class LoadNewScene : MonoBehaviour
    {
        GameObject savingObject = null;
        [SerializeField] GameObject savedScene;

        private void Start()
        {
            savingObject = GameObject.FindGameObjectWithTag("Saving");

        }

        // change to specific scene
        public void ChangeScene(int scene)
        {
            // if  I want the game to save on scene change
          /* if (savingObject)
            {
                savingObject.GetComponent<SavingWrapper>().Save();
            }*/
            
            SceneManager.LoadScene(scene);
        }

        // change scene to one previously saved
        public void LoadSavedScene()
        {
            SceneManager.LoadScene(savedScene.GetComponent<SaveScene>().savedScene);
        }

      
    }

   
}

