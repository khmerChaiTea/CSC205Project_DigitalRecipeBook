using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecipeBook
{
	// Entry point for the Digital Recipe Book application
	public class Program
	{
		// Instance of RecipeBook to manage recipes
		private static RecipeBook recipeBook = new RecipeBook();

		// File path for saving and loading recipe data
		private static string filePath = "recipes.json";

		// Main method to run the application
		public static void Main()
		{
			// Load recipe data from file or create a new RecipeBook if file does not exist
			recipeBook = RecipeManager.LoadRecipeData(filePath) ?? new RecipeBook();

			Console.WriteLine("Welcome to the Digital Recipe Book!");

			// Main loop to present options to the user
			bool running = true;
			while (running)
			{
				Console.WriteLine("\nChoose an option:");
				Console.WriteLine("1. Add a new recipe");
				Console.WriteLine("2. List all recipes");
				Console.WriteLine("3. Find recipes by category");
				Console.WriteLine("4. Remove a recipe");
				Console.WriteLine("5. Save and exit");

				// Read the user's choice
				string? choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						AddRecipe(); // Call method to add a new recipe
						break;
					case "2":
						ListRecipes(); // Call method to list all recipes
						break;
					case "3":
						FindRecipesByCategory(); // Call method to find recipes by category
						break;
					case "4":
						RemoveRecipe(); // Call method to remove a recipe
						break;
					case "5":
						SaveAndExit(); // Call method to save data and exit
						running = false; // Exit the loop
						break;
					default:
						Console.WriteLine("Invalid choice, please try again."); // Handle invalid input
						break;
				}
			}
		}

		// Method to add a new recipe
		private static void AddRecipe()
		{
			// Get recipe name from the user
			string name = GetRecipeName();

			// Get recipe category from the user
			RecipeType category = GetRecipeCategory();

			// Create a new Recipe instance
			var recipe = new Recipe(name, category);

			// Add ingredients to the recipe
			AddIngredientsToRecipe(recipe);

			// Update cooking instructions for the recipe
			UpdateRecipeInstructions(recipe);

			// Add the newly created recipe to the recipe book
			recipeBook.AddRecipe(recipe);
			Console.WriteLine("Recipe added successfully!");
		}

		// Method to prompt user for the recipe name
		private static string GetRecipeName()
		{
			Console.Write("Enter recipe name: ");
			return Console.ReadLine() ?? throw new InvalidOperationException("Recipe name cannot be null.");
		}

		// Method to prompt user for the recipe category
		private static RecipeType GetRecipeCategory()
		{
			Console.WriteLine("Choose a category:");
			foreach (var category in Enum.GetValues(typeof(RecipeType)))
			{
				Console.WriteLine($"{(int)category}. {category}");
			}

			int categoryChoice;
			if (!int.TryParse(Console.ReadLine(), out categoryChoice) || !Enum.IsDefined(typeof(RecipeType), categoryChoice))
			{
				Console.WriteLine("Invalid category choice.");
				throw new InvalidOperationException("Invalid category choice.");
			}

			return (RecipeType)categoryChoice;
		}

		// Method to prompt user for ingredients and add them to the recipe
		private static void AddIngredientsToRecipe(Recipe recipe)
		{
			bool addingIngredients = true;
			while (addingIngredients)
			{
				Console.Write("Enter ingredient name (or type 'done' to finish): ");
				string? ingredientName = Console.ReadLine();
				if (ingredientName?.ToLower() == "done")
				{
					addingIngredients = false; // Exit ingredient addition loop
				}
				else
				{
					// Validate and prompt for ingredient quantity
					if (string.IsNullOrWhiteSpace(ingredientName))
					{
						Console.WriteLine("Ingredient name cannot be null or empty.");
						continue;
					}

					Console.Write("Enter ingredient quantity: ");
					string quantity = Console.ReadLine() ?? throw new InvalidOperationException("Ingredient quantity cannot be null.");

					if (string.IsNullOrWhiteSpace(quantity))
					{
						Console.WriteLine("Ingredient quantity cannot be null or empty.");
						continue;
					}

					// Add ingredient to the recipe
					recipe.AddIngredient(new Ingredient(ingredientName, quantity));
				}
			}
		}

		// Method to prompt user for cooking instructions and update the recipe
		private static void UpdateRecipeInstructions(Recipe recipe)
		{
			Console.Write("Enter the instructions: ");
			recipe.UpdateInstructions(Console.ReadLine() ?? throw new InvalidOperationException("Instructions cannot be null."));
		}

		// Method to list all recipes in the recipe book
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
					Console.WriteLine($"- {recipe.RecipeName}"); // Display each recipe name
				}
			}
		}

		// Method to find recipes by category
		private static void FindRecipesByCategory()
		{
			Console.WriteLine("Choose a category:");
			foreach (var category in Enum.GetValues(typeof(RecipeType)))
			{
				Console.WriteLine($"{(int)category}. {category}"); // Display available categories
			}

			int categoryChoice;
			if (!int.TryParse(Console.ReadLine(), out categoryChoice) || !Enum.IsDefined(typeof(RecipeType), categoryChoice))
			{
				Console.WriteLine("Invalid category choice.");
				return;
			}

			RecipeType selectedCategory = (RecipeType)categoryChoice;

			// Find and display recipes in the selected category
			var recipes = recipeBook.FindRecipeByCategory(selectedCategory);
			if (recipes.Count == 0)
			{
				Console.WriteLine("No recipes found in this category.");
			}
			else
			{
				foreach (var recipe in recipes)
				{
					Console.WriteLine($"- {recipe.RecipeName}"); // Display each recipe name in the selected category
				}
			}
		}

		// Method to remove a recipe by name
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

		// Method to save recipe data to a file and exit the application
		private static void SaveAndExit()
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new InvalidOperationException("File path cannot be null or empty.");
			}

			// Save the recipe data to the specified file
			RecipeManager.SaveRecipeData(filePath, recipeBook);
			Console.WriteLine("Recipes saved successfully! Goodbye!");
		}
	}
}
