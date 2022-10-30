using System.Collections.Generic;
using System.Linq;

namespace Items
{
    public class ItemType
    {
        public static readonly Ingredient Strawberry = new("Strawberry");
        public static readonly Ingredient Lemon = new("Lemon");

        public static readonly Cocktail CocktailFire = new("CocktailFire");
        public static readonly Cocktail CocktailIce = new("CocktailIce");

        public static readonly List<Item> Values = new()
        {
            Strawberry,
            Lemon,
            CocktailFire,
            CocktailIce
        };

        public static Item GetItem(string name)
        {
            return Values.FirstOrDefault(item => item.Name == name);
        }
    }
}