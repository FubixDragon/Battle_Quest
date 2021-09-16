using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Core;
using Fubix.Combat;

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] Weapon item = null;
    [SerializeField] GameObject pickUpEffect = null;
    GameObject Log;

    private void Start()
    {
        Log = GameObject.FindGameObjectWithTag("ConsoleLog");
    }

    private void OnTriggerEnter(Collider other)
    {
        Log.GetComponent<printLog>().consoleLog("Picked Up " + item.name);
        other.GetComponent<PlayerInventory>().addItem(item.name);
        if (pickUpEffect) Instantiate(pickUpEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
