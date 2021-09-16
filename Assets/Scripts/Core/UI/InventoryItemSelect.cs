using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fubix.Combat;
using Fubix.Core;


public class InventoryItemSelect : MonoBehaviour
{

    GameObject Player = null;
    GameObject itemSelectIndacator;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        itemSelectIndacator = GameObject.FindGameObjectWithTag("itemSelectIndacator");
    }

    // this gameObject is a prefab Button. However, when it gets created it is given the 
    // name of the item/Weapon associated with the Button.
    public void assignItem()
    {
        // assigns "chooseWeapon" in PlayerFighter for when item is being assigned to desired 
        // combat controls.
        Player.GetComponent<PlayerFighter>().chooseWeapon = this.gameObject.name;

        // assigns "itemChoosen" in PlayerInventory for deletion when deleteItem() is called.
        Player.GetComponent<PlayerInventory>().itemChoosen = this.gameObject;

        Weapon itemIcon = Player.GetComponent<PlayerFighter>().GetItemSelect(this.gameObject.name);
        itemSelectIndacator.GetComponent<Image>().sprite = itemIcon.GetIconImage();
    }

 

}
