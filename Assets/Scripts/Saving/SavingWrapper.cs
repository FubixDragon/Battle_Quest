using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fubix.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "gameSaveFightLocationProblem1.1";

        private void Awake()
        {
            Load();
        }


   


        private void Update()
        {
           
           
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                Delete();
            }
        }

       

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }
    }

}