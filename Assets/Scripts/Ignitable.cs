using System.Collections;
using Items;
using UnityEngine;

public class Ignitable : MonoBehaviour, IThrowCocktailTrigger, IInteractionCondition
{
    public GameObject firePrefab;
    public GameObject cocktailPrefab;

    public int deleteAfterSeconds = 3;

    private bool _onFire;
    private SoundManager SM;

    public void Start()
    {
        SM = GameObject.Find("SM_SE").GetComponent<SoundManager>();
    }

    public bool CanInteract(Interactor interactor)
    {
        return !_onFire;
    }

    public GameObject CocktailPrefab => cocktailPrefab;

    public bool AcceptsCocktail(Cocktail cocktail)
    {
        return cocktail.Name == ItemType.CocktailLemPep.Name || cocktail.Name == ItemType.CocktailStrawPep.Name;
    }

    public void OnCocktailHit(Interactor interactor)
    {
        Instantiate(firePrefab, transform.position, Quaternion.identity);
        _onFire = true;
        SM.playSoundEffect(4);
        SM.playSoundEffect(19);
        StartCoroutine(WaitDelete());
    }

    private IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(deleteAfterSeconds);
        if (isActiveAndEnabled)
        {
            SM.playSoundEffect(24);
            Destroy(gameObject);
        }
    }
}