using System.Collections;
using UnityEngine;

public class Ignitable : MonoBehaviour, IInteractingTrigger, IInteractionCondition
{
    public GameObject firePrefab;
    public GameObject cocktailPrefab;

    public int deleteAfterSeconds = 3;

    private bool _onFire;

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
        cocktailThrowing.onHit += OnFire;
    }

    public bool CanInteract(Interactor interactor)
    {
        return !_onFire;
    }

    private void OnFire()
    {
        Instantiate(firePrefab, transform.position, Quaternion.identity);
        _onFire = true;
        StartCoroutine(WaitDelete());
    }

    private IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(deleteAfterSeconds);
        if (isActiveAndEnabled) Destroy(gameObject);
    }
}