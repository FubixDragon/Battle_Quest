using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Saving;
using UnityEngine.UI;
using Fubix.Core;

namespace Fubix.Looks
{
    public class PlayerLooks : MonoBehaviour, ISaveable
    {
        [SerializeField] GameObject[] HairStyles;
        [SerializeField] GameObject[] BodyStyles;
        [SerializeField] GameObject[] LegStyles;


        int currentHairStyle = 0;
        int currentBodyStyle = 0;
        int currentLegStyle = 1;

        string colorOfSkin = "";
         Material skin;

        string colorOfHair = "";
         Material hair;

        string colorOfShirt = "";
         Material shirt;

        string colorOfLegs = "";
         Material leg;


        // defualt race colors
        //-----------------------------

        [SerializeField] Material defualtSkin;
        [SerializeField] Material defualtHair;
        [SerializeField] Material defualtLeg;
        [SerializeField] Material defualtBody;

        GameObject Log;
        private void Start()
        {
            Log = GameObject.FindGameObjectWithTag("ConsoleLog");
        }

        public void changeHairForward()
        {

            // remove old Hair Style
            HairStyles[currentHairStyle].SetActive(false);

            // bring up next currentHairStyle
            currentHairStyle += 1;
            if (currentHairStyle > HairStyles.Length - 1) currentHairStyle = 0;

            // show new Hair Style
            HairStyles[currentHairStyle].SetActive(true);

            // after changing body parts, skin resets to default color
            // so change all skin to choosen color
            changeSkinColor(skin);
        }

        public void changeBodyForward()
        {
            // remove old body Style
            BodyStyles[currentBodyStyle].SetActive(false);

            // bring up next Body Style
            currentBodyStyle += 1;
            if (currentBodyStyle > BodyStyles.Length - 1) currentBodyStyle = 0;

            // show new Body Style
            BodyStyles[currentBodyStyle].SetActive(true);

            // after changing body parts, skin resets to default color
            // so change all skin to choosen color
            changeSkinColor(skin);
        }

        public void changeLegForward()
        {
            // remove old Leg Style
            LegStyles[currentLegStyle].SetActive(false);

            // bring up next Leg Style
            currentLegStyle += 1;
            if (currentLegStyle > LegStyles.Length - 1) currentLegStyle = 0;

            // show new Leg Style
            LegStyles[currentLegStyle].SetActive(true);

            // after changing body parts, skin resets to default color
            // so change all skin to choosen color
            changeSkinColor(skin);
        }


        public void changeHairColor(Material HairName)
        {
            if (HairStyles[0].activeSelf)
            {
                Material[] HairColor = { skin };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = HairColor;
                colorOfHair = HairName.name;
            }
            if (HairStyles[1].activeSelf)
            {
                Material[] HairColor = { skin, HairName };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = HairColor;
                colorOfHair = HairName.name;
            }
            if (HairStyles[2].activeSelf)
            {
                Material[] HairColor = { skin, HairName };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = HairColor;
                colorOfHair = HairName.name;
            }
            if (HairStyles[3].activeSelf)
            {
                Material[] HairColor = { HairName, skin };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = HairColor;
                colorOfHair = HairName.name;

            }
            hair = HairName;
        }


        public void changePantColor(Material PantName)
        {
         
            if (LegStyles[0].activeSelf)
            {
                Material[] LegColor = { PantName, skin };// second item are the boots. LegSyles[0] = pants/w boots
                LegStyles[currentLegStyle].GetComponent<SkinnedMeshRenderer>().materials = LegColor;
                colorOfLegs = PantName.name;
            }
            if (LegStyles[1].activeSelf)
            {
                Material[] LegColor = { PantName, skin };
                LegStyles[currentLegStyle].GetComponent<SkinnedMeshRenderer>().materials = LegColor;
                colorOfLegs = PantName.name;
            }
            leg = PantName;
        }


        public void changeSkinColor(Material SkinName)
        {
            Material[] SkinColor = { shirt, SkinName, SkinName };
            BodyStyles[currentBodyStyle].GetComponent<SkinnedMeshRenderer>().materials = SkinColor;
            colorOfSkin = SkinName.name;
            skin = SkinName;

            changeStatsBasedOnSkinColor(SkinName.name);

            // after changing skin color, must reset skin color on other body parts
            changeHairColor(hair);
            changePantColor(leg);
            changeShirtColor(shirt);

            // change race discripton
          
        }

        public void changeStatsBasedOnSkinColor(string skinName)
        {
            // change stats according to skin color(race)

            switch (skinName)
            {
                // setStats(   maxHP, maxEnd, maxSpi, STR, FOC, SPD   )

                case "HumanLight":
                    GetComponent<Stats>().setStats(5, 5, 5, 5, 5, 5, "Amari Human");
                    break;

                case "HumanTan":
                    GetComponent<Stats>().setStats(10, 5, 5, 10, 0, 0, "Duni Human");
                    break;

                case "HumanDark":
                    GetComponent<Stats>().setStats(0, 0, 10, 0, 10, 10, "Qwuem Human");
                    break;

            }
        }


        public void changeShirtColor(Material ShirtName)
        {

            if (BodyStyles[0].activeSelf)
            {
                Material[] ShirtColor = { ShirtName, skin, skin };
                BodyStyles[currentBodyStyle].GetComponent<SkinnedMeshRenderer>().materials = ShirtColor;
                colorOfShirt = ShirtName.name;
            }
            if (BodyStyles[1].activeSelf)
            {
                Material[] ShirtColor = { ShirtName, skin };
                BodyStyles[currentBodyStyle].GetComponent<SkinnedMeshRenderer>().materials = ShirtColor;
                colorOfShirt = ShirtName.name;
            }
            shirt = ShirtName;
        }

        private void SetSkin(Dictionary<string, object> Looks)
        {
            colorOfSkin = "Attire/Skin/" + (string)Looks["SkinColor"];
            skin = Resources.Load<Material>(colorOfSkin);
            // reset to the shortend name because after saving the system can find it otherwise
            colorOfSkin = skin.name;
        }
        private void SetBodyParts(Dictionary<string, object> Looks)
        {
            BodyStyles[(int)Looks["BodyStyle"]].SetActive(true);

            colorOfShirt = "Attire/Body/" + (string)Looks["ShirtColor"];

            shirt = Resources.Load<Material>(colorOfShirt);

            currentBodyStyle = (int)Looks["BodyStyle"];

            if (BodyStyles[0].activeSelf)
            {
                Material[] skinColor = { shirt, skin, skin };
                BodyStyles[currentBodyStyle].GetComponent<SkinnedMeshRenderer>().materials = skinColor;
            }
            if (BodyStyles[1].activeSelf)
            {
                Material[] skinColor = { shirt, skin, skin };
                BodyStyles[currentBodyStyle].GetComponent<SkinnedMeshRenderer>().materials = skinColor;
            }
            // reset to the shortend name because after saving the system can find it otherwise
            colorOfShirt = shirt.name;
        }

        private void SetLegParts(Dictionary<string, object> Looks)
        {
            LegStyles[(int)Looks["LegStyle"]].SetActive(true);

            colorOfLegs = "Attire/Legs/" + (string)Looks["LegColor"];

            leg = Resources.Load<Material>(colorOfLegs);

            currentLegStyle = (int)Looks["LegStyle"];

            if (LegStyles[0].activeSelf)
            {
                Material[] LegColor = { leg, skin };// second item are the boots. LegSyles[0] = pants/w boots
                LegStyles[currentLegStyle].GetComponent<SkinnedMeshRenderer>().materials = LegColor;
            }
            if (LegStyles[1].activeSelf)
            {
                Material[] LegColor = { leg, skin };
                LegStyles[currentLegStyle].GetComponent<SkinnedMeshRenderer>().materials = LegColor;
            }
            // reset to the shortend name because after saving the system can find it otherwise
            colorOfLegs = leg.name;
        }
        private void SetHeadStyle(Dictionary<string, object> Looks)
        {
            HairStyles[(int)Looks["HairStyle"]].SetActive(true);

            // load materials of hair/head
            colorOfHair = "Attire/Hair/" + (string)Looks["HairColor"];
            hair = Resources.Load<Material>(colorOfHair);
            // set materials
            currentHairStyle = (int)Looks["HairStyle"];


            if (HairStyles[0].activeSelf)
            {
                Material[] hairColor = { skin };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = hairColor;
            }
            if (HairStyles[1].activeSelf)
            {
                Material[] hairColor = { skin, hair };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = hairColor;
            }
            if (HairStyles[2].activeSelf)
            {
                Material[] hairColor = { skin, hair };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = hairColor;
            }
            if (HairStyles[3].activeSelf)
            {
                Material[] hairColor = { hair, skin };
                HairStyles[currentHairStyle].GetComponent<SkinnedMeshRenderer>().materials = hairColor;
            }
            // reset to the shortend name because after saving the system can find it otherwise
            colorOfHair = hair.name;
        }


        // sets everything to the defualt set.
        public void resetCharacter()
        {
            for(int i = 0; i < HairStyles.Length; i++)
            {
                HairStyles[1].SetActive(false);
            }

            for(int b = 0; b < BodyStyles.Length; b++)
            {
                BodyStyles[b].SetActive(false);
            }

            for(int c = 0; c < LegStyles.Length; c++)
            {
                LegStyles[c].SetActive(false);
            }
        }

        public void startNewCharacter(Dictionary<string, object> bodyChoices, Material hairColor, Material skinColor)
        {
           

            // set global styles to defaults of new Character
            currentHairStyle = (int)bodyChoices["hairStyle"];
            currentBodyStyle = (int)bodyChoices["bodyStyle"];
            currentLegStyle = (int)bodyChoices["legStyle"];
            colorOfHair = hairColor.name;
            colorOfSkin = skinColor.name;

            // set defualt new character body styles as active
            HairStyles[(int)bodyChoices["hairStyle"]].SetActive(true);
            BodyStyles[(int)bodyChoices["bodyStyle"]].SetActive(true);
            LegStyles[(int)bodyChoices["legStyle"]].SetActive(true);

            // change colors to defualts of new character
            changeHairColor(hairColor);
            changeSkinColor(skinColor);
          //  changeShirtColor(shirt);
          //  changePantColor(leg);
        }

        public void setDefualtCharacter()
        {
            resetCharacter();
            Dictionary<string, object> bodyChoices = new Dictionary<string, object>();

            bodyChoices["hairStyle"] = 1;
            bodyChoices["bodyStyle"] = 0;
            bodyChoices["legStyle"] = 1;

            leg = defualtLeg;
            shirt = defualtBody;

            startNewCharacter(bodyChoices, defualtHair, defualtSkin);

            
        }




        public object CaptureState()
        {
            Dictionary<string, object> Looks = new Dictionary<string, object>();

           if (currentHairStyle > -1) Looks["HairStyle"] = currentHairStyle;
           if (currentBodyStyle > -1) Looks["BodyStyle"] = currentBodyStyle;
           if (currentLegStyle > -1) Looks["LegStyle"] = currentLegStyle;

           if (colorOfSkin != "") Looks["SkinColor"] = colorOfSkin;
           if (colorOfHair != "") Looks["HairColor"] = colorOfHair;
           if (colorOfShirt != "") Looks["ShirtColor"] = colorOfShirt;
           if (colorOfLegs != "") Looks["LegColor"] = colorOfLegs;

            Looks["FirstTime"] = false;

            return Looks;
        }

        public void RestoreState(object state)
        {
           
            Dictionary<string, object> Looks = (Dictionary<string, object>)state;

            if (Looks.ContainsKey("HairStyle") && Looks.ContainsKey("BodyStyle") &&
                Looks.ContainsKey("LegStyle") && Looks.ContainsKey("SkinColor") &&
                Looks.ContainsKey("HairColor") && Looks.ContainsKey("ShirtColor") &&
                Looks.ContainsKey("LegColor"))
            {
                SetSkin(Looks);
                SetBodyParts(Looks);
                SetLegParts(Looks);
                SetHeadStyle(Looks);
            }




        }


    }
}

