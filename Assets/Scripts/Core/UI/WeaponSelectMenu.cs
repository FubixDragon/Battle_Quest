using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Fubix.Combat;

namespace Fubix.Core
{
    public class WeaponSelectMenu : MonoBehaviour
    {
        [SerializeField] GameObject Player = null;
        [SerializeField] GameObject menu = null;
        [SerializeField] Button inventoryItemListing = null;

        Vector2 itemPos;

        private void Awake()
        {
            createInventoryItem();
        }
        // creates a button to represent inventory item equipable to a controller combo trigge
        public void createInventoryItem()
        {
            // get the players inventory
            Dictionary<string, object> inventory = Player.GetComponent<PlayerInventory>().getInventory();
            // set starting point for item list creation
            itemPos.y = menu.transform.position.y;

            foreach (KeyValuePair<string, object> item in inventory)
            {
                // create button and set its position inside designated menu container
                itemPos = new Vector2(menu.transform.position.x, itemPos.y -= 80);
                Button newButton = Instantiate(inventoryItemListing);

                // set button parameters
                newButton.GetComponentInChildren<Text>().text = item.Value.ToString();
                newButton.name = item.Value.ToString();
                newButton.transform.SetParent(menu.transform, false);
                newButton.transform.position = itemPos;    
            }
        }

        public void openMenu()
        {
            this.gameObject.SetActive(true);
            Player.GetComponent<PlayerAreaExplorererController>().enabled = false;
            Player.GetComponent<NavMeshAgent>().enabled = false;
        }

        public void closeMenu()
        {
            this.gameObject.SetActive(false);
            Player.GetComponent<PlayerAreaExplorererController>().enabled = true;
            Player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}

