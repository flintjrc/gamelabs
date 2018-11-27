using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torchPickUp : MonoBehaviour, IInventoryItem {

    public string Name
    {
        get { return _Name; }
    }

    public Sprite Image
    {
        get { return _Image; }
    }

    public void OnPickup()
    {
        //Add action when we pickup camera
        gameObject.SetActive(false);
    }

    public Sprite _Image = null;

    public string _Name = "Torch";
}
