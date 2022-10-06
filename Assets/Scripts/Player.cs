using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Events;

[DisallowMultipleComponent]
// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour
{
    public readonly Inventory inventory = new Inventory(4);
    public int health;
    public bool shaker = false;
    public bool dead = false;
    public UnityEvent GameOver;

    
    [SerializeField] private InputActionReference _shakeActionReference;

    public void Start()
    {
        health = 5;
        
        _shakeActionReference.action.Enable();
        _shakeActionReference.action.performed += OnShake;
    }

    public void Update()
    {
        if(health <= 0 && !dead)
        {
            GameOver.Invoke();
        }
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
