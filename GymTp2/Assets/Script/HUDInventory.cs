using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDInventory : MonoBehaviour {

    public Inventory Inventory;

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                image.name = e.Item.Name;
                break;
            }

        }
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (image.name != e.Item.Name) continue;

            image.enabled = false;
            image.sprite = null;
            image.name = null;
            break;

        }
    }
}
