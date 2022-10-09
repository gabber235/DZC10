using UnityEngine;

public class Pipe : MonoBehaviour, IInteractionCondition, IInteractingTrigger
{
    public GameObject cocktailPrefab;

    public int cocktailsRequired = 1;
    public int cocktailsAdded;

    public int CocktailsLeft => cocktailsRequired - cocktailsAdded;

    public void OnInteract(Interactor interactor)
    {
        if (cocktailsAdded >= cocktailsRequired)
            return;
        // If a player damages the enemy, we remove a cocktail from their inventory and damage the enemy.
        var player = interactor.GetComponent<Player>();
        if (player == null) return;
        var inventory = player.inventory;
        var cocktailItem = inventory.GetFirstCocktail();
        if (cocktailItem == null) return;

        if (cocktailPrefab == null)
        {
            Debug.LogError("No cocktail prefab set on pipe!");
            return;
        }

        inventory.RemoveItem(cocktailItem.Name);

        var cocktail = Instantiate(cocktailPrefab, interactor.transform.position, Quaternion.identity);
        var cocktailThrowing = cocktail.GetComponent<CocktailThrowing>();
        cocktailThrowing.Target = transform;
        // This is ran once the animation is done.
        cocktailThrowing.onHit += AddCocktail;
    }

    public bool CanInteract(Interactor interactor)
    {
        return cocktailsAdded < cocktailsRequired;
    }

    private void AddCocktail()
    {
        cocktailsAdded++;
    }
}