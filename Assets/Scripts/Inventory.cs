using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventory system to track items in a player's inventory
public class Inventory
{
    public List<Item> Items { get; } = new();

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
    
    public void AddItemByType(ItemType type)
    {
        Item item = new Item(type);
        Items.Add(item);
    }
    
    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
}

public class Item
{
    public Item(ItemType type)
    {
        this.Type = type;
    }

    public ItemType Type { get; }
}

public enum ItemType
{
    Berry,
    Pineapple,
    Banana,
    Coconut,
}

// An extension for everything that may hold an inventory
public interface IInventoryHolder
{
    public Inventory Inventory { get; }
}