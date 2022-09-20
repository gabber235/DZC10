using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour, IInventoryHolder
{
    public Inventory Inventory { get; private set; }

    public void Start()
    {
        Inventory = new Inventory();
    }
}
