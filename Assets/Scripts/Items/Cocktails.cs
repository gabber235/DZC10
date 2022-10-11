// Cocktail maker to make cocktails from items in the inventory

using System.Collections.Generic;
using System.Linq;

public class Cocktails
{
    private static readonly List<CocktailRecipe> Recipes = new()
    {
        new CocktailRecipe(ItemType.CocktailStrawLem, ItemType.Strawberry, ItemType.Lemon),
        //new CocktailRecipe(ItemType.Cocktail, ItemType.Berry, ItemType.Coconut),
        new CocktailRecipe(ItemType.CocktailStrawPep, ItemType.Strawberry, ItemType.Pepper),
        //new CocktailRecipe(ItemType.Cocktail, ItemType.Pineapple, ItemType.Coconut),
        new CocktailRecipe(ItemType.CocktailLemPep, ItemType.Lemon, ItemType.Pepper),
        //new CocktailRecipe(ItemType.Cocktail, ItemType.Coconut, ItemType.Banana),
    };

    public class CocktailRecipe
    {
        public readonly Ingredient[] Ingredients;
        public readonly Cocktail Result;

        public CocktailRecipe(Cocktail result, params Ingredient[] ingredients)
        {
            Ingredients = ingredients;
            Result = result;
        }
    }
    
    public static CocktailRecipe MatchRecipe(Inventory inventory)
    {
        return Recipes.FirstOrDefault(recipe => recipe.Ingredients.All(item => inventory.Items.Contains(item.Name)));
    }
}