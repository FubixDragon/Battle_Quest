using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFileChecker : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Button Button1;
    [SerializeField] Button Button2;


    private void Awake()
    {
        
    }

    private void Update()
    {
        if (player.GetComponent<Stats>().GetClass() != "none")
        {
          //  print(player.GetComponent<Stats>().GetClass());
            Button1.gameObject.SetActive(true);
            Button2.gameObject.SetActive(false);
        }
        else
        {
            Button1.gameObject.SetActive(false);
        }
    }
}
