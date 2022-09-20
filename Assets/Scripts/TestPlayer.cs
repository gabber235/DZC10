using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sample player to quickly test the inventory system.
public class TestPlayer : MonoBehaviour, IInventoryHolder
{
    public Inventory Inventory { get; private set; }

    public void Start()
    {
        Inventory = new Inventory();
    }

    private void FixedUpdate()
    {
        foreach (var item in Inventory.Items)
        {
            Debug.Log(item.Type);
        }
    }
}
