using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fubix.Saving;
using UnityEngine.UI;

namespace Fubix.Core
{
    public class PlayerInventory : MonoBehaviour, ISaveable
    {
        public GameObject itemChoosen;
        Dictionary<string, object> inventory = new Dictionary<string, object>();
        GameObject Log;

        private void Start()
        {
            Log = GameObject.FindGameObjectWithTag("ConsoleLog");
        }

        public void addItem(string item)
        {
            inventory[item] = item;

        }

        public void removeItem()
        {
            // itemChoosen get its value when an inventory item sets it from outside.
            // Or from another source.
            inventory.Remove(itemChoosen.name);
            Destroy(itemChoosen);
            Log.GetComponent<printLog>().consoleLog("Trashed " + itemChoosen.name);
        }


        public Dictionary<string, object> getInventory()
        {
            return inventory;
        }



        public object CaptureState()
        {
           // Log.GetComponent<printLog>().consoleLog("Changes Saved");
            return inventory;
        }

        public void RestoreState(object state)
        {
           inventory = (Dictionary<string, object>)state;
        }

       
    }

}
