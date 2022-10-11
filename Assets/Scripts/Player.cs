using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Events;

[DisallowMultipleComponent]
// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour
{
    public int health;
    public bool dead = false;
    public UnityEvent GameOver;

    public double lastDamTime;
    
    [HideInInspector] public bool shaker;
    [HideInInspector] public double lastDamTime;

    [SerializeField] private InputActionReference shakeActionReference;
    public readonly Inventory Inventory = new(4);

    public void Start()
    {
        health = 5;

        shakeActionReference.action.Enable();
        shakeActionReference.action.performed += OnShake;
    }

    private void OnShake(InputAction.CallbackContext context)
    {
        if (!shaker) return;
        Inventory.MakeCockTail();
    }

    public void Damage(int damage)
    {
        health -= damage;
        // last dam time --> currrent playing time
        lastDamTime = Time.realtimeSinceStartup;
    }
}