using UnityEngine;

public class Pipe : MonoBehaviour, IInteractionCondition, IThrowCocktailTrigger
{
    public GameObject cocktailPrefab;

    public int cocktailsRequired = 1;
    public int cocktailsAdded;

    public int CocktailsLeft => cocktailsRequired - cocktailsAdded;

    private SoundManager SM;

    public void Start(){
        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    public bool CanInteract(Interactor interactor)
    {
        return cocktailsAdded < cocktailsRequired;
    }

    public GameObject CocktailPrefab => cocktailPrefab;

    public void OnCocktailHit(Interactor interactor)
    {
        cocktailsAdded++;
        SM.playSoundEffect(18);
        SM.playSoundEffect(25);
        if(cocktailsAdded == cocktailsRequired){
            SM.playSoundEffect(26);
        }
    }
}