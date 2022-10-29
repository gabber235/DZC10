using System;
using System.Collections.Generic;
using System.Linq;
using Tutorial;
using Object = UnityEngine.Object;

// Inventory system to track items in a player's inventory
public class Inventory
{
    private readonly int _maxItems;
    private readonly int _playerID;

    public Inventory(int maxItems, int playerID)
    {
        _maxItems = maxItems;
        _playerID = playerID;
    }

    public List<string> Items { get; } = new();

    private bool HasMaxItems => Items.Count >= _maxItems;

    public bool AddItem(string name)
    {
        // If a inventory is full, remove the first ingredient
        if (HasMaxItems)
        {
            var firstIngredient = Items.First(item => ItemType.GetItem(item) is Ingredient);
            if (firstIngredient != null)
                Items.Remove(firstIngredient);
            else return false;
        }

        Items.Add(name);


        // For the tutorial, we complete the PickupMore if the player has two different ingredients in their inventory
        if (Items.ToHashSet().Count >= 2)
            Object.FindObjectOfType<TutorialManager>()?.FinishStep(TutorialStep.PickupMore, _playerID, true);

        return true;
    }

    public Cocktail GetFirstCocktail(Func<Cocktail, bool> predicate)
    {
        return Items.Select(ItemType.GetItem).OfType<Cocktail>().FirstOrDefault(predicate);
    }

    public void RemoveItem(string name)
    {
        Items.Remove(name);
    }

    public void MakeCockTail()
    {
        var recipe = Cocktails.MatchRecipe(this);
        if (recipe == null)
            return;
        // Remove all the ingredients from the inventory
        foreach (var ingredient in recipe.Ingredients)
        {
            var item = Items.FirstOrDefault(i => i == ingredient.Name);
            if (item != null)
                Items.Remove(item);
        }

        // Add the cocktail to the inventory
        AddItem(recipe.Result.Name);
    }
}