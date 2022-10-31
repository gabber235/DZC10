using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Waterpuddle : MonoBehaviour, IThrowCocktailTrigger, IInteractionCondition
{
    public GameObject cocktailPrefab;
    public GameObject waterPuddleTrigger;
    public GameObject frozenPuddle;
    public GameObject waterPuddleCollider;
    public GameObject Electricity;

    private SoundManager _sm;

    private void Start()
    {
        _sm = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    public bool CanInteract(Interactor interactor){
        return true;
    }

    public GameObject CocktailPrefab => cocktailPrefab;

    public bool AcceptsCocktail(Cocktail cocktail){
        return cocktail.Name == ItemType.CocktailIce.Name;
    }

    public void OnCocktailHit(Interactor interactor){
        _sm.playSoundEffect(10);
        frozenPuddle.SetActive(true);
        waterPuddleCollider.SetActive(false);
        waterPuddleTrigger.SetActive(false);
        Electricity.SetActive(false);
        gameObject.SetActive(false);
    }
}
