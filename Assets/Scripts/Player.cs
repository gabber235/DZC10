using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour
{
    public readonly Inventory inventory = new Inventory(4);
    public int health;
    public bool shaker = false;
    
    [SerializeField] private InputActionReference _shakeActionReference;

    public void Start()
    {
        health = 5;
        
        _shakeActionReference.action.Enable();
        _shakeActionReference.action.performed += OnShake;
    }
    
    private void OnShake(InputAction.CallbackContext context)
    {
        if (!shaker) return;
        inventory.MakeCockTail();
    }

    public void Damage(int damage)
    {
        health -= damage;
    }
}
