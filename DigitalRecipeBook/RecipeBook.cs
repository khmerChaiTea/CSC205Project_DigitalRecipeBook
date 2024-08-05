using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalRecipeBook
{
    // Represents a collection of recipes and provides methods to manage them
    public class RecipeBook
    {
        // List of recipes in the recipe book
        public List<Recipe> Recipes { get; set; }

        // Constructor to initialize the recipe book with an empty list of recipes
        public RecipeBook()
        {
            Recipes = new List<Recipe>();
        }

        // Adds a new recipe to the recipe book
        public void AddRecipe(Recipe recipe)
        {
            // Check if the recipe name is null, empty, or consists only of white spaces
            if (string.IsNullOrWhiteSpace(recipe.RecipeName))
            {
                Console.WriteLine("Recipe name cannot be empty or null.");
                return; // Do not add the recipe
            }

            // Check if a recipe with the same name already exists
            var existingRecipe = Recipes.Find(r => r.RecipeName.Equals(recipe.RecipeName, StringComparison.OrdinalIgnoreCase));
            if (existingRecipe == null)
            {
                Recipes.Add(recipe); // Add the recipe to the list
            }
            else
            {
                Console.WriteLine($"A recipe with the name '{recipe.RecipeName}' already exists.");
            }
        }

        // Finds a recipe by its name
        public Recipe? FindRecipeByName(string recipeName)
        {
            // Search for the recipe by name, ignoring case, and return it if found
            return Recipes.FirstOrDefault(r => r.RecipeName.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
        }

        // Finds all recipes that belong to a specific category
        public List<Recipe> FindRecipeByCategory(RecipeType category)
        {
            // Filter recipes by the specified category and return the list
            return Recipes.Where(r => r.Category == category).ToList();
        }

        // Removes a recipe by its name from the recipe book
        public void RemoveRecipe(string recipeName)
        {
            // Find the recipe by name
            var recipe = FindRecipeByName(recipeName);
            if (recipe != null)
            {
                Recipes.Remove(recipe); // Remove the recipe from the list if found
            }
        }
    }
}
