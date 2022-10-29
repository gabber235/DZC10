using System.Linq;
using Items;
using QuickOutline;
using UnityEngine;

[DisallowMultipleComponent]
public class Interactable : MonoBehaviour
{
    public float outlineWidth = 1f;

    private Interactor[] _interactors;

    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        if (_outline == null) _outline = gameObject.AddComponent<Outline>();

        _outline.OutlineMode = Outline.Mode.OutlineVisible;
        _outline.OutlineWidth = 0f;

        _interactors = FindObjectsOfType<Interactor>();
    }

    // Update is called once per frame
    private void Update()
    {
        var interactors = _interactors.Where(i => i.Interactable == this);

        var enumerable = interactors.ToList();
        var isTargeted = enumerable.Any();

        if (isTargeted)
        {
            using var enumerator = enumerable.GetEnumerator();
            enumerator.MoveNext();
            if (enumerator.Current != null)
            {
                var color = enumerator.Current.interactableColor;

                while (enumerator.MoveNext()) color = Color.Lerp(color, enumerator.Current.interactableColor, 0.5f);

                _outline.OutlineColor = color;
            }
        }


        _outline.OutlineWidth = Mathf.Lerp(_outline.OutlineWidth, isTargeted ? outlineWidth : 0, Time.deltaTime * 40f);
    }
}


internal interface IInteractingTrigger
{
    void OnInteract(Interactor interactor);
}

internal interface IInteractionCondition
{
    bool CanInteract(Interactor interactor);
}

internal interface IThrowCocktailTrigger : IInteractingTrigger
{
    GameObject CocktailPrefab { get; }

    // Make a exception in naming to align with the GameObject.transform
    // ReSharper disable once InconsistentNaming
    Transform transform { get; }

    void IInteractingTrigger.OnInteract(Interactor interactor)
    {
        // If a player damages the enemy, we remove a cocktail from their inventory and damage the enemy.
        var player = interactor.GetComponent<Player>();
        if (player == null) return;
        var inventory = player.Inventory;
        var cocktailItem = inventory.GetFirstCocktail(AcceptsCocktail);
        if (cocktailItem == null) return;

        var cocktailPrefab = CocktailPrefab;

        if (cocktailPrefab == null) return;

        inventory.RemoveItem(cocktailItem.Name);

        var cocktail = Object.Instantiate(cocktailPrefab, interactor.transform.position, Quaternion.identity);
        var cocktailThrowing = cocktail.GetComponent<CocktailThrowing>();
        cocktailThrowing.Target = transform;
        // This is ran once the animation is done.
        cocktailThrowing.onHit += () => { OnCocktailHit(interactor); };
    }

    bool AcceptsCocktail(Cocktail cocktail)
    {
        return true;
    }


    void OnCocktailHit(Interactor interactor);
}