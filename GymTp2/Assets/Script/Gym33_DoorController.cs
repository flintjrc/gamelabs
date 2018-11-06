using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gym33_DoorController : MonoBehaviour {

	// Use this for initialization
    public Inventory Inventory;
    public string keyName;
    public string id;

	void Start ()
	{
	    Gym33_CollisionController.CollideADoor += CheckOpenADoor;
	}

    private void CheckOpenADoor(object sender, CollideADoorEventArgs e)
    {
        if(Inventory.HasItem(keyName) && this.id == e.Id)
        {
            GetComponent<Animator>().Play("DoorOpening");
            Inventory.RemoveItem(Inventory.GetItem(keyName));
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}
