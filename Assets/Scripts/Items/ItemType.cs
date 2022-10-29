using System.Collections.Generic;
using System.Linq;
using Items;

public class ItemType
{
    public static readonly Ingredient Strawberry = new("strawberry");
    public static readonly Ingredient Lemon = new("Lemon");
    public static readonly Ingredient Pepper = new("Pepper");

    public static readonly Cocktail CocktailStrawPep = new("CocktailStrawPep");
    public static readonly Cocktail CocktailLemPep = new("CocktailLemPep");
    public static readonly Cocktail CocktailStrawLem = new("CocktailStrawLem");

    public static readonly List<Item> Values = new()
    {
        Strawberry,
        Lemon,
        Pepper,
        CocktailStrawPep,
        CocktailLemPep,
        CocktailStrawLem
    };

    public static Item GetItem(string name)
    {
        return Values.FirstOrDefault(item => item.Name == name);
    }
}