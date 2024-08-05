using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecipeBook
{
    public class Program
    {
        private static RecipeBook recipeBook = new RecipeBook(); // RecipeBook instance
        private static string filePath = "recipes.json"; // File path for recipes

        public static async Task Main()
        {
            // Load recipes from file
            RecipeBook loadedRecipeBook = await RecipeManager.LoadRecipeDataAsync(filePath);
            recipeBook = loadedRecipeBook ?? new RecipeBook();

            Console.WriteLine("Welcome to the Digital Recipe Book!");

            bool running = true;
            while (running)
            {
                // Display menu options
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add a new recipe");
                Console.WriteLine("2. List all recipes");
                Console.WriteLine("3. Find recipes by category");
                Console.WriteLine("4. Remove a recipe");
                Console.WriteLine("5. Save and exit");

                // Read user choice
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddRecipe();
                        break;
                    case "2":
                        ListRecipes();
                        break;
                    case "3":
                        FindRecipesByCategory();
                        break;
                    case "4":
                        RemoveRecipe();
                        break;
                    case "5":
                        await SaveAndExit();
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private static void AddRecipe()
        {
            string name = GetRecipeName(); // Get recipe name

            // Check for duplicate recipe name
            if (recipeBook.Recipes.Any(r => r.RecipeName.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"A recipe with the name '{name}' already exists.");
                return; // Exit the method if a duplicate is found
            }

            RecipeType category = GetRecipeCategory(); // Get category
            var recipe = new Recipe(name, category);

            AddIngredientsToRecipe(recipe); // Add ingredients
            UpdateRecipeInstructions(recipe); // Update instructions

            recipeBook.AddRecipe(recipe);
            Console.WriteLine("Recipe added successfully!");
        }

        private static string GetRecipeName()
        {
            while (true)
            {
                Console.Write("Enter recipe name: ");
                string? name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    return name;
                }
                Console.WriteLine("Recipe name cannot be null or empty. Please try again.");
            }
        }

        private static RecipeType GetRecipeCategory()
        {
            while (true)
            {
                Console.WriteLine("Choose a category:");
                foreach (var category in Enum.GetValues(typeof(RecipeType)))
                {
                    Console.WriteLine($"{(int)category}. {category}");
                }

                if (int.TryParse(Console.ReadLine(), out int categoryChoice) && Enum.IsDefined(typeof(RecipeType), categoryChoice))
                {
                    return (RecipeType)categoryChoice;
                }
                Console.WriteLine("Invalid category choice. Please try again.");
            }
        }

        private static void AddIngredientsToRecipe(Recipe recipe)
        {
            bool addingIngredients = true;
            while (addingIngredients)
            {
                Console.Write("Enter ingredient name: ");
                string? ingredientName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingredientName) || ingredientName.Contains("\n") || ingredientName.Contains("\r"))
                {
                    Console.WriteLine("Ingredient name cannot be null, empty, or multi-line. Please try again.");
                }
                else
                {
                    Console.Write("Enter ingredient quantity: ");
                    string? quantity = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(quantity) || quantity.Contains("\n") || quantity.Contains("\r"))
                    {
                        Console.WriteLine("Ingredient quantity cannot be null, empty, or multi-line. Please try again.");
                    }
                    else
                    {
                        recipe.AddIngredient(new Ingredient(ingredientName, quantity)); // Add valid ingredient

                        // Ask if the user wants to add another ingredient
                        bool validResponse = false;
                        while (!validResponse)
                        {
                            Console.Write("Do you want to add another ingredient? (y/n): ");
                            string? response = Console.ReadLine();
                            if (response?.ToLower() == "y")
                            {
                                validResponse = true;
                            }
                            else if (response?.ToLower() == "n")
                            {
                                addingIngredients = false; // Stop adding ingredients
                                validResponse = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid response. Please enter 'y' for yes or 'n' for no.");
                            }
                        }
                    }
                }
            }
        }

        private static void UpdateRecipeInstructions(Recipe recipe)
        {
            Console.WriteLine("Enter the instructions (type 'done' on a new line to finish):");
            StringBuilder instructions = new StringBuilder();
            while (true)
            {
                string? line = Console.ReadLine();
                if (line == null || line.Trim().ToLower() == "done")
                {
                    break;
                }
                instructions.AppendLine(line);
            }
            recipe.UpdateInstructions(instructions.ToString().Trim());
        }

        private static void ListRecipes()
        {
            if (recipeBook.Recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
            }
            else
            {
                foreach (var recipe in recipeBook.Recipes)
                {
                    Console.WriteLine($"- {recipe.RecipeName}"); // List recipe names
                }
            }
        }

        private static void FindRecipesByCategory()
        {
            Console.WriteLine("Choose a category:");
            foreach (var category in Enum.GetValues(typeof(RecipeType)))
            {
                Console.WriteLine($"{(int)category}. {category}");
            }

            if (int.TryParse(Console.ReadLine(), out int categoryChoice) && Enum.IsDefined(typeof(RecipeType), categoryChoice))
            {
                RecipeType selectedCategory = (RecipeType)categoryChoice;
                var recipes = recipeBook.FindRecipeByCategory(selectedCategory);

                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found in this category.");
                }
                else
                {
                    foreach (var recipe in recipes)
                    {
                        Console.WriteLine($"- {recipe.RecipeName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid category choice.");
            }
        }

        private static void RemoveRecipe()
        {
            Console.Write("Enter the name of the recipe to remove: ");
            string? recipeName = Console.ReadLine();
            if (recipeName == null)
            {
                Console.WriteLine("Recipe name cannot be null.");
                return;
            }

            recipeBook.RemoveRecipe(recipeName);
            Console.WriteLine("Recipe removed successfully!");
        }

        private static async Task SaveAndExit()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new InvalidOperationException("File path cannot be null or empty.");
            }

            // Save recipes to file
            await RecipeManager.SaveRecipeDataAsync(filePath, recipeBook);
            Console.WriteLine("Recipes saved successfully! Goodbye!");
        }
    }
}
