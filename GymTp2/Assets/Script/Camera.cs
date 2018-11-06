using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour, IInventoryItem {

    public string Name
    {
        get { return "Camera"; }
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
