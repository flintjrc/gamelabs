using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorKeyControl : MonoBehaviour {

	// Use this for initialization
	// Use this for initialization
	// Use this for initialization
    public Inventory Inventory;
    public string keyName;


	void Start ()
	{
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
	}

    public void openDoor()
    {
		Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        if(Inventory.HasItem(keyName))
        {
            this.GetComponent<Animator>().Play("DoorOpening");
            Inventory.RemoveItem(Inventory.GetItem(keyName));
        }
    }
}
