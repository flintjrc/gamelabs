using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int slots = 6;
    [SerializeField] public List<IInventoryItem> menuItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventoryItem item)
    {
        if (menuItems.Count < slots)
        {
            //Adds item when the player collides with them
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (collider.enabled)
            {
                collider.enabled = false;
                menuItems.Add(item);
                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }

    public void RemoveItem(IInventoryItem item)
    {
        if (ItemRemoved != null && item != null)
        {
            ItemRemoved(this, new InventoryEventArgs(item));
        }
    }

    public bool HasItem(string name)
    {
        return this.menuItems.FirstOrDefault(item => item.Name == name) != null;
    }

    public IInventoryItem GetItem(string name)
    {
        return this.menuItems.FirstOrDefault(item => item.Name == name);
    }
}
