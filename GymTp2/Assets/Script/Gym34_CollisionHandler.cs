using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym34_CollisionHandler : MonoBehaviour {
    public Inventory Inventory;

    private void OnTriggerEnter(Collider collision)
    {
        //If there's an IInventory component attached, it adds it to the inventory
        Debug.Log("Collision");
        IInventoryItem item = collision.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Inventory.AddItem(item);
        }
    }
}
