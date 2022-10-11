using UnityEngine;

public class Pipe : MonoBehaviour, IInteractionCondition, IThrowCocktailTrigger
{
    public GameObject cocktailPrefab;

    public int cocktailsRequired = 1;
    public int cocktailsAdded;

    public int CocktailsLeft => cocktailsRequired - cocktailsAdded;

    public bool CanInteract(Interactor interactor)
    {
        return cocktailsAdded < cocktailsRequired;
    }

    public GameObject CocktailPrefab => cocktailPrefab;

    public void OnCocktailHit(Interactor interactor)
    {
        cocktailsAdded++;
    }
}