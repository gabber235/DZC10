using System.Collections.Generic;
using System.Linq;

public class ItemType
{
    public static readonly Ingredient Berry = new("Berry");
    public static readonly Ingredient Pineapple = new("Pineapple");
    public static readonly Ingredient Banana = new("Banana");
    public static readonly Ingredient Coconut = new("Coconut");
    
    public static readonly Cocktail Cocktail = new("Cocktail");
    
    public static readonly List<Item> Values = new()
    {
        Berry,
        Pineapple,
        Banana,
        Coconut,
        Cocktail
    };
    
    public static Item GetItem(string name)
    {
        return Values.FirstOrDefault(item => item.Name == name);
    }
}