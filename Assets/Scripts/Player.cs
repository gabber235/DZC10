using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
// Sample player to quickly test the inventory system.
public class Player : MonoBehaviour
{
    public int health;

    [HideInInspector] public bool shaker;
    [HideInInspector] public double lastDamTime;

    [SerializeField] private InputActionReference shakeActionReference;
    [HideInInspector] public bool dead;
    public UnityEvent GameOver;

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
        if (dead) return;
        health -= damage;

        if (health <= 0)
        {
            health = 0;
            dead = true;
            GameOver.Invoke();
        }

        // last dam time --> currrent playing time
        lastDamTime = Time.realtimeSinceStartup;
    }
}