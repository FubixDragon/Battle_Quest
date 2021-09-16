using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Saving;

namespace Fubix.Looks
{
    public class Attire : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject Hair1 = null;
        [SerializeField] GameObject Hair2 = null;
        [SerializeField] GameObject Hair3 = null;
        [SerializeField] GameObject Hair4 = null;

        [SerializeField] GameObject Pants = null;
        [SerializeField] GameObject Shorts = null;

        [SerializeField] GameObject LongShirt = null;
        [SerializeField] GameObject Shirt = null;


        GameObject Head = null;




        void Awake()
        {
            CheckHairModel();
            CheckLegsModel();
            CheckTorsoModel();
        }

        // These check what Model Parts are current and active
        private void CheckTorsoModel()
        {
            if (LongShirt.activeSelf)
            {
                print("Long Shirt");
            }
            if (Shirt.activeSelf)
            {
                print("Short Shirt");
            }
        }

        private void CheckLegsModel()
        {
            if (Pants.activeSelf)
            {
                print("Pants");
            }
            if (Shorts.activeSelf)
            {
                print("Shorts");
            }
        }

        private void CheckHairModel()
        {
            if (Hair1.activeSelf)
            {
                Head = Hair1;
            }
            if (Hair2.activeSelf)
            {
                Head = Hair2;
            }
            if (Hair3.activeSelf)
            {
                Head = Hair3;
            }
            if (Hair4.activeSelf)
            {
                Head = Hair4;
            }
        }
       


        // Saving and setting body Choices on load
        public object CaptureState()
        {
            Dictionary<string, object> Attire = new Dictionary<string, object>();

            Attire["Skin"] = "WhiteSkin";
            Attire["Head"] = "RedSkin";
            Attire["Legs"] = "ShinyBluePants";

            return Attire;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> Attire = (Dictionary<string, object>)state;

            string skinName = "Attire/Skin/" + (string)Attire["Skin"];
            string hairName = "Attire/Head/" + (string)Attire["Head"];
            string pantName = "Attire/Legs/" + (string)Attire["Legs"];

            Material skin = Resources.Load<Material>(skinName);
            Material headMateriel = Resources.Load<Material>(hairName);
            Material pantMateriel = Resources.Load<Material>(pantName);


            // sets head materials
            Material[] headArr = new Material[3];
            if (Hair1.activeSelf)// if hair 1(bald) is set
            {
                // set only one materiels (bald doesn't have hair)
                headArr[0] =  headMateriel;
            }
            else // if any of the other hair models are set
            {
                headArr[0] = skin; // set skin material
                headArr[1] = headMateriel;// set Hair Material
            }

            

            // sets leg materials
            Material[] pantsArr = { pantMateriel, pantMateriel };

            Head.GetComponent<SkinnedMeshRenderer>().materials = headArr;
            Pants.GetComponent<SkinnedMeshRenderer>().materials = pantsArr;


        }
    }
}
