using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fubix.Saving;

public class SaveScene : MonoBehaviour, ISaveable
{
    [SerializeField] bool saveSceneLocation = true;
    public int savedScene = 0;

    public object CaptureState()
    {
        if(saveSceneLocation)
        {
            savedScene = SceneManager.GetActiveScene().buildIndex;
        }
        
        return savedScene;
    }

    public void RestoreState(object state)
    {
        savedScene = (int)state;
    }


}
