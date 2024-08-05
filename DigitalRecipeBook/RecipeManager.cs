using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DigitalRecipeBook
{
    // Provides methods for saving and loading recipe data to and from a file
    public static class RecipeManager
    {
        // Asynchronously saves the recipe data to a file
        public static async Task SaveRecipeDataAsync(string filePath, RecipeBook recipeBook)
        {
            try
            {
                // Filter out recipes with valid names
                var validRecipes = recipeBook.Recipes
                    .Where(r => !string.IsNullOrWhiteSpace(r.RecipeName))
                    .ToList();

                // Identify and log invalid recipes
                var invalidRecipes = recipeBook.Recipes
                    .Where(r => string.IsNullOrWhiteSpace(r.RecipeName))
                    .ToList();

                if (invalidRecipes.Any())
                {
                    Console.WriteLine("Some recipes were invalid and will not be saved:");
                    foreach (var recipe in invalidRecipes)
                    {
                        Console.WriteLine($"Invalid Recipe (null or empty name): {JsonConvert.SerializeObject(recipe)}");
                    }
                }

                // Serialize the valid recipes to JSON and save to file
                var json = JsonConvert.SerializeObject(new RecipeBook { Recipes = validRecipes }, Formatting.Indented);
                await File.WriteAllTextAsync(filePath, json);
                Console.WriteLine("Recipe data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving recipe data: {ex.Message}");
            }
        }

        // Asynchronously loads the recipe data from a file
        public static async Task<RecipeBook> LoadRecipeDataAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new RecipeBook(); // Return a new RecipeBook if the file doesn't exist
            }

            try
            {
                // Read the JSON data from the file
                var json = await File.ReadAllTextAsync(filePath);
                var recipeBook = JsonConvert.DeserializeObject<RecipeBook>(json);

                if (recipeBook == null || recipeBook.Recipes == null)
                {
                    return new RecipeBook(); // Return a new RecipeBook if deserialization fails
                }

                // Filter and validate recipes
                var validRecipes = recipeBook.Recipes
                    .Where(r => !string.IsNullOrWhiteSpace(r.RecipeName))
                    .ToList();

                if (recipeBook.Recipes.Count != validRecipes.Count)
                {
                    Console.WriteLine("Some recipes were skipped due to missing names:");
                    foreach (var recipe in recipeBook.Recipes)
                    {
                        if (string.IsNullOrWhiteSpace(recipe.RecipeName))
                        {
                            Console.WriteLine($"Skipped Recipe (null or empty name): {JsonConvert.SerializeObject(recipe)}");
                        }
                    }
                }

                return new RecipeBook { Recipes = validRecipes };
            }
            catch (JsonSerializationException jsonEx)
            {
                Console.WriteLine($"Error deserializing JSON: {jsonEx.Message}");
                return new RecipeBook(); // Return an empty RecipeBook on error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading recipe data: {ex.Message}");
                return new RecipeBook(); // Return an empty RecipeBook on error
            }
        }
    }
}
