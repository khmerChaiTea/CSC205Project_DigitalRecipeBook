using System;

namespace DigitalRecipeBook
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Create a recipe
			var pancakeRecipe = new Recipe("Pancakes", RecipeType.MainCourse);
			pancakeRecipe.AddIngredient(new Ingredient("Flour", "2 cups"));
			pancakeRecipe.AddIngredient(new Ingredient("Milk", "1.5 cups"));
			pancakeRecipe.UpdateInstructions("Mix ingredients and cook on skillet.");

			// Display the recipe
			DisplayRecipe(pancakeRecipe);
		}

		public static void DisplayRecipe(Recipe recipe)
		{
            Console.WriteLine($"Recipe: {recipe.RecipeName}");
			Console.WriteLine($"Category: {recipe.Category}");
			Console.WriteLine("Ingredients:");
			foreach (var ingredient in recipe.Ingredients)
			{
				Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity}");
			}
            Console.WriteLine($"Instructions: {recipe.Instructions}");
        }
	}
}
