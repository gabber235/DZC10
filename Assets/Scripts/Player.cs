using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour, IInventoryHolder
{
    public Inventory Inventory { get; private set; }
    public int health;
    
    [SerializeField] private InputActionReference _attackActionReference;

    public void Start()
    {
        Inventory = new Inventory(4);
        health = 5;
        
        _attackActionReference.action.Enable();
        _attackActionReference.action.performed += OnShake;
    }
    
    private void OnShake(InputAction.CallbackContext context)
    {
        Debug.Log("Shake");
        Inventory.MakeCockTail();
    }
}
