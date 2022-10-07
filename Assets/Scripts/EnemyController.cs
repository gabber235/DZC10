using UnityEngine;

public class EnemyController : MonoBehaviour, IInteractingTrigger
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    public GameObject cocktailPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void OnInteract(Interactor interactor)
    {
        // If a player damages the enemy, we remove a cocktail from their inventory and damage the enemy.
        var player = interactor.GetComponent<Player>();
        if (player == null) return;
        var inventory = player.inventory;
        var cocktailItem = inventory.GetFirstCocktail();
        if (cocktailItem == null) return;

        if (cocktailPrefab == null) return;

        inventory.RemoveItem(cocktailItem.Name);

        var cocktail = Instantiate(cocktailPrefab, interactor.transform.position, Quaternion.identity);
        var cocktailThrowing = cocktail.GetComponent<CocktailThrowing>();
        cocktailThrowing.Target = transform;
        // This is ran once the animation is done.
        cocktailThrowing.onHit += () => { Damage(100); };
    }

    private void Damage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth == 0) EnemyDeath();
    }

    private void EnemyDeath()
    {
        // this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}