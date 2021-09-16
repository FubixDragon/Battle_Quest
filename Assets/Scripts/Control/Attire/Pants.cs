using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Saving;

public class Pants : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject Legs = null;

    string pants = "ShinyBluePants";

    

    void Awake()
    {
       
    }

    public object CaptureState()
    {

        return pants;
    }

    public void RestoreState(object state)
    {
        print("wearing test");
        string pantName = "Attire/Legs/" + (string)state;
        print(pantName + "wearing");
        Material pantMateriel = Resources.Load<Material>(pantName);
        Legs.GetComponent<SkinnedMeshRenderer>().material = pantMateriel;
       
    }


}
