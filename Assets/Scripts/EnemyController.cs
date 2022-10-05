using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IInteractingTrigger {

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    
    // Start is called before the first frame update
    void Start() {
        _currentHealth = _maxHealth;
    }

    public void Damage(float damage) {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth == 0) {
            EnemyDeath();
        }
    }

    public void OnInteract(Interactor interactor)
    {
        // If a player damages the enemy, we remove a cocktail from their inventory and damage the enemy.
        var player = interactor.GetComponent<Player>();
        if (player == null) return;
        var inventory = player.inventory;
        var cocktail = inventory.GetFirstCocktail();
        if (cocktail == null) return;
        
        Damage(75);
        
        inventory.RemoveItem(cocktail.Name);
    }

    private void EnemyDeath() {
        // this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
