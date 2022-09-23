using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

// Inventory system to track items in a player's inventory
public class Inventory
{
    private readonly int _maxItems;

    public Inventory(int maxItems)
    {
        _maxItems = maxItems;
    }

    public List<string> Items { get; } = new();

    private bool HasMaxItems => Items.Count >= _maxItems;

    public bool AddItem(string name)
    {
        // If a inventory is full, remove the first ingredient
        if (HasMaxItems)
        {
            if(ItemType.GetItem(Items[0]) is Ingredient)
                Items.RemoveAt(0);
            else return false;
        }
        Items.Add(name);
        return true;
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

// An extension for everything that may hold an inventory
public interface IInventoryHolder
{
    public Inventory Inventory { get; }
}