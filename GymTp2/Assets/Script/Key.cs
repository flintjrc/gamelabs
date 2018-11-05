using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInventoryItem
{

    public string Name
    {
        get { return "Key"; }
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
}
