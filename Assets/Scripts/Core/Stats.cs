using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Saving;
using Fubix.Core;
using Fubix.Combat;

public class Stats : MonoBehaviour, ISaveable
{
    [SerializeField] GameObject deadScreen = null;
    [SerializeField] float Health = 10f;
    [SerializeField] float maxHealth = 10f;

    [SerializeField] float Endurance = 10f;
    [SerializeField] float maxEndurance = 10f;

    [SerializeField] float Spirit = 10f;
    [SerializeField] float maxSpirit = 10f;

    [SerializeField] float Strength = 1f;
    [SerializeField] float Focus = 1f;
    [SerializeField] float Speed = 1f;

    [SerializeField] string ClassType = "none";
    [SerializeField] string ElementalType = "none";
    [SerializeField] string ElementWeakness = "none";

    private int currency = 0;
    [SerializeField] int rewardAmount = 0;
    [SerializeField] Weapon item;


    private string baseRace = "";

    private string meleeClass = "Melee";
    private string magicClass = "Magic";
    private string altClass1 = "???";
    private string altClass2 = "!!!";

    float baseBodyStats = 10;
    float baseAbilityStats = 2;

    int characterScore = 0;


    private void Update()
    {
        // what ever gameObject this script is attached to, when
        // it reaches 0 or less HP will bring up the dialog screen
        // attached to it. For player its the Game Over screen,
        // for an enemy its the win screen.

        // If Game Over screen is brought up, the close button also
        // deletes any saved file and rerstarts the game.
        if (Health <= 0 && deadScreen != null)
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

            // stop fighters from fighting
            player.GetComponent<PlayerController>().controlsEneabled = false;
            enemy.GetComponent<AiController>().controlsEneabled = false;

            // do death animation when this objects health reaches 0
            this.GetComponent<Animator>().SetTrigger("Death");

            deadScreen.SetActive(true);

            // give reward, if the player died then it doesn't matter
            // cause the file is going to be deleted anyway.

            // Currency reward
            if(rewardAmount > 0)
            {
                player.GetComponent<Stats>().addCurrency(rewardAmount);
            }

            // item reward
            if(item != null)
            {
                player.GetComponent<PlayerInventory>().addItem(item.name);
            }
        }
    }

    public void characterAnswer(int score)
    {
        characterScore += score;

        switch (characterScore)
        {
            case 0:
                baseTypeChooser(altClass1);
                break;
            case 1:
                baseTypeChooser(meleeClass);
                break;
            case 2:
                baseTypeChooser(meleeClass);
                break;
            case 3:
                baseTypeChooser(meleeClass);
                break;
            case 4:
                baseTypeChooser(meleeClass);
                break;
            case 5:
                baseTypeChooser(magicClass);
                break;
            case 6:
                baseTypeChooser(magicClass);
                break;
            case 7:
                baseTypeChooser(magicClass);
                break;
            case 8:
                baseTypeChooser(magicClass);
                break;
            case 9:
                baseTypeChooser(altClass2);
                break;
        }
    }
    private void baseTypeChooser(string classType)
    {
        float randType = Random.Range(0, 6);
        float randWeakness = Random.Range(0, 6);

        // Random ElementalType Type
        switch (randType)
        {
            case 0:
                ElementalType = "Fire";
                break;
            case 1:
                ElementalType = "Water";
                break;
            case 2:
                ElementalType = "Earth";
                break;
            case 3:
                ElementalType = "Electric";
                break;
            case 4:
                ElementalType = "Negative";
                break;
            case 5:
                ElementalType = "Positive";
                break;

        }

        // Random Elemental Weakness
        switch (randWeakness)
        {
            case 0:
                ElementWeakness = "Fire";
                break;
            case 1:
                ElementWeakness = "Water";
                break;
            case 2:
                ElementWeakness = "Earth";
                break;
            case 3:
                ElementWeakness = "Electric";
                break;
            case 4:
                ElementWeakness = "Negative";
                break;
            case 5:
                ElementWeakness = "Positive";
                break;

        }

        ClassType = classType;
    }

    public void addCurrency(int amount)
    {
        currency += amount;
    }
    public void addHealth(float damage, string type)
    {
        if (type == "Heal")
        {
            Health += damage;
        }
        // if dmg type is same as my type and my weakness is same as my type
        // basically rare type: my type and weakness are the same
        else if (type == ElementalType && ElementWeakness == ElementalType)
        {
            // heal instead of take damage.
            Health -= damage;
        }
        // if dmg type is same as my type but my weakness is not same as my type
        // basically common type: my type and weakness are not the same
        else if (type == ElementalType && ElementWeakness != ElementalType)
        {
            // do half damage
            Health += (damage / 2);
        }

        //if dmg type is my weakness
        else if (type == ElementWeakness)
        {
            // deal double damage
            Health += (damage * 2);
        }

        // if dmg type is not my weakness
        else if (type != ElementalType)
        {
            //just do normal damage
            Health += damage;
        }

    }
    public void addEndurance(float drain, string type)
    {
        float enduranceCost = drain;
        if (ClassType == altClass1)
        {
            // make something cool happen
        }
        else if (type != ClassType)
        {
            // double cost
            enduranceCost = -(drain * drain);
            Endurance += enduranceCost;
            // also make this move cost half as much spirit
            addSpirit((drain / 2), ClassType);
        }
        else
        {
            // if move matches class then cost is normal
            Endurance += enduranceCost;
        }

    }
    public void addSpirit(float drain, string type)
    {
        float spiritCost = drain;
        if (ClassType == altClass1)
        {
            // make something cool happen
        }
        else if (type != ClassType)
        {
            spiritCost = -(drain * drain);
            Spirit += spiritCost;
            addEndurance((drain / 2), ClassType);
        }
        else
        {
            Spirit += spiritCost;
        }

    }


    public void addMaxHealth(float addHealth)
    {
        maxHealth += addHealth;
    }
    public void addMaxEndurance(float addEndurance)
    {
        maxEndurance += addEndurance;
    }
    public void addMaxSpirit(float addSpirit)
    {
        maxSpirit += addSpirit;
    }

    public void addStrength(float bonus)
    {
        Strength += bonus;
    }
    public void addFocus(float bonus)
    {
        Focus += bonus;
    }
    public void addSpeed(float bonus)
    {
        Speed += bonus;
    }
    public void setStats(float hp, float end, float spi, float str, float foc, float spd, string race)
    {
        baseRace = race;

        maxHealth = hp + baseBodyStats;
        maxEndurance = end + baseBodyStats;
        maxSpirit = spi + baseBodyStats;


        Health = maxHealth;
        Endurance = maxEndurance;
        Spirit = maxSpirit;

        Strength = str + baseAbilityStats;
        Focus = foc + baseAbilityStats;
        Speed = spd + baseAbilityStats;
      
    }



    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetMaxEndurance()
    {
        return maxEndurance;
    }
    public float GetMaxSpirit()
    {
        return maxSpirit;
    }

    public float GetSpirit()
    {
        return Spirit;
    }
    public float GetEndurance()
    {
        return Endurance;
    }
    public float GetHealth()
    {
        return Health;
    }

    public float GetStrength()
    {
        return Strength;
    }
    public float GetFocus()
    {
        return Focus;
    }
    public float GetSpeed()
    {
        return Speed;
    }

    public string GetRace()
    {
        return baseRace;
    }
    public string GetClass()
    {
        return ClassType;

    }
    public string GetElementalType()
    {
        return ElementalType;
    }
    public string GetWeakness()
    {
        return ElementWeakness;
    }


    public string GetClassBasicMessage()
    {

        return "Hello Adventurer, my name is Sifu Yupe. I am a Basic Style Master, which" +
            " is a style type in what is known as Kung Fu. I see that you have completed" +
            " the Class and Type Evaluation test. Its says here that you are " + ClassType +
            ", with an energy type of "+ ElementalType + ", and a weakness to "+ ElementWeakness +
            ". Hmmm... interseting, I haven't seen traites like these in a while, it must" +
             " be your personality. I can help you develope your power." +
            " Come to my Training Studio and I will teach you some Basic Style Kung Fu to get" +
            " you started.";
    }

    public string GetClassMessageAltClass1()
    {
        return "I've never seen results like these before! I don't know what this means for" +
            " you or how to better instruct you. My advice to you would be to train hard and maybe" +
            " the secrets to your class will reveal themselves. Good luck. Your Element type is " +
            ElementalType + ". As such your elemental weakness is " + ElementWeakness;
    }

    public string GetClassMessageAltClass2()
    {
        return "I've seen results like these before. I won't instruct you on how to develop " +
            "this power, it is far too dangerous. My advice to you would be to find peace " +
            "with in yourself and to always remeber, Power corrupts. Your Element type is " +
             ElementalType + ". As such your elemental weakness is " + ElementWeakness; ;
    }


    public object CaptureState()
    {
        Dictionary<string, object> data = new Dictionary<string, object>();

        data["Health"] = Health;
        data["maxHealth"] = maxHealth;

        data["Endurance"] = Endurance;
        data["maxEndurance"] = maxEndurance;

        data["Spirit"] = Spirit;
        data["maxSpirit"] = maxSpirit;


        data["Strength"] = Strength;
        data["Focus"] = Focus;
        data["Speed"] = Speed;

        data["ClassType"] = ClassType;
        data["ElementalType"] = ElementalType;
        data["ElementWeakness"] = ElementWeakness;

        return data;
    }


    public void RestoreState(object state)
    {
        Dictionary<string, object> data = (Dictionary<string, object>)state;

        Health = (float)data["Health"];
        maxHealth = (float)data["maxHealth"];

        Endurance = (float)data["Endurance"];
        maxEndurance = (float)data["maxEndurance"];

        Spirit = (float)data["Spirit"];
        maxSpirit = (float)data["maxSpirit"];

        Strength = (float)data["Strength"];
        Focus = (float)data["Focus"];
        Speed = (float)data["Speed"];

        ClassType = (string)data["ClassType"];
        ElementalType = (string)data["ElementalType"];
        ElementWeakness = (string)data["ElementWeakness"];

       
    }
}


