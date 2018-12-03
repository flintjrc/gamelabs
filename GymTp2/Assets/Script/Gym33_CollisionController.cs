using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym33_CollisionController : MonoBehaviour
{

    public Inventory Inventory;

    public static event EventHandler<CollideADoorEventArgs> CollideADoor;

    private void OnTriggerEnter(Collider collision)
    {
        //If there's an IInventory component attached, it adds it to the inventory
        IInventoryItem item = collision.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Inventory.AddItem(item);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door" || collision.gameObject.tag == "DoorWithKeyLevel2" && CollideADoor != null)
        {
            string id = collision.gameObject.transform.parent.gameObject.GetComponent<Gym33_DoorController>().id;
            CollideADoor(this, new CollideADoorEventArgs(id));
        }
    }
}

public class CollideADoorEventArgs : EventArgs
{

    public CollideADoorEventArgs(string id)
    {
        Id = id;
    }

    public string Id;
}