using System;
using UnityEngine.UI;
using UnityEngine;

namespace Fubix.Core
{
    public class StatDisplay : MonoBehaviour
    {
        Stats playerStats;

        [SerializeField] string chooseStat;


        void Awake()
        {
            playerStats = GameObject.FindWithTag("Player").GetComponent<Stats>();
        }

        void Update()
        {

            GetComponent<Text>().text = getPlayerStat(chooseStat);
        }

        private string getPlayerStat(string statName)
        {
            string statValue = "";
            switch (statName)
            {
                case "current race":
                    statValue = playerStats.GetRace();
                    break;
                case "current health":
                    statValue = playerStats.GetHealth().ToString();
                    break;
                case "current endurance":
                    statValue = playerStats.GetEndurance().ToString();
                    break;
                case "current spirit":
                    statValue = playerStats.GetSpirit().ToString();
                    break;
                //------------------------
                case "max health":
                    statValue = playerStats.GetMaxHealth().ToString();
                    break;
                case "max endurance":
                    statValue = playerStats.GetMaxEndurance().ToString();
                    break;
                case "max spirit":
                    statValue = playerStats.GetMaxSpirit().ToString();
                    break;
                //------------------------
                case "strength":
                    statValue = playerStats.GetStrength().ToString();
                    break;
                case "focus":
                    statValue = playerStats.GetFocus().ToString();
                    break;
                case "speed":
                    statValue = playerStats.GetSpeed().ToString();
                    break;
                //------------------------
                case "ClassType":
                    statValue = playerStats.GetClass().ToString();
                    break;
                case "ElementalType":
                    statValue = playerStats.GetElementalType().ToString();
                    break;
                case "Weakness":
                    statValue = playerStats.GetWeakness().ToString();
                    break;
                case "ClassMessageMelee":
                    statValue = classMessage();
                    break;


            }
            return statValue;
        }

        // gets the explaination of the specific class
        private string classMessage()
        {
            string classType = playerStats.GetClass();
            string message = "";

            switch (classType)
            {
                case "Melee":
                    message = playerStats.GetClassBasicMessage().ToString();
                    break;
                case "Magic":
                    message = playerStats.GetClassBasicMessage().ToString();
                    break;
                case "???":
                    message = playerStats.GetClassMessageAltClass1().ToString();
                    break;
                case "!!!":
                    message = playerStats.GetClassMessageAltClass2().ToString();
                    break;
            }
            return message;
        }


    }
}
