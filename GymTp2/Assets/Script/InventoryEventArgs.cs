using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup(); //Called when object is pickedup by the player
}

public class InventoryEventArgs : EventArgs
{

    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}
