using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithLever : MonoBehaviour {

    // Use this for initialization
    [SerializeField] bool open = false;

    [SerializeField] bool moving = false;

    public void toggleDoor(bool toggle)
    {
        open = toggle;
        if (open)
        {
            GetComponent<Animator>().Play("DoorOpening");
        }
        else
        {
            GetComponent<Animator>().Play("DoorClosing");
        }
        
        moving = true;
    }
}
