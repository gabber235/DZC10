using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(Player))]
public class PlayerAttacking : MonoBehaviour {

    [SerializeField] private Transform _attackPos;
    [SerializeField] private float _range;
    
    // Coop stuff
    [SerializeField] private InputActionReference _attackActionReference;

    private Vector3 test;
    // Start is called before the first frame update
    void Start() {
        _attackActionReference.action.Enable();

        _attackActionReference.action.performed += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext obj) {
        var player = GetComponent<Player>();
        var inventory = player.inventory;
        var cocktail = inventory.GetFirstCocktail();
        if (cocktail == null) return;


        var enemyCols = Physics.OverlapSphere(_attackPos.position, _range);
        var hasHit = false;

        foreach (var col in enemyCols)
        {
            if (!col.CompareTag("Enemy")) continue;
            col.GetComponent<EnemyController>().UpdateHealth(75);
            hasHit = true;
        }
        
        if (hasHit) {
            inventory.RemoveItem(cocktail.Name);
        }
    }
}
