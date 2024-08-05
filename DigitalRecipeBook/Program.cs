using System;

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
			// Prompt user to enter the recipe name
			Console.Write("Enter recipe name: ");
			string name = Console.ReadLine() ?? throw new InvalidOperationException("Recipe name cannot be null.");

			// Prompt user to choose a category from a list of available categories
			Console.WriteLine("Choose a category:");
			foreach (var category in Enum.GetValues(typeof(RecipeType)))
			{
				Console.WriteLine($"{(int)category}. {category}");
			}

			// Read and validate category choice
			int categoryChoice;
			if (!int.TryParse(Console.ReadLine(), out categoryChoice) || !Enum.IsDefined(typeof(RecipeType), categoryChoice))
			{
				Console.WriteLine("Invalid category choice.");
				return;
			}

			RecipeType selectedCategory = (RecipeType)categoryChoice;

			// Create a new Recipe instance
			var recipe = new Recipe(name, selectedCategory);

			bool addingIngredients = true;
			while (addingIngredients)
			{
				// Prompt user to enter an ingredient or finish adding ingredients
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

			// Prompt user to enter cooking instructions
			Console.Write("Enter the instructions: ");
			recipe.UpdateInstructions(Console.ReadLine() ?? throw new InvalidOperationException("Instructions cannot be null."));

			// Add the newly created recipe to the recipe book
			recipeBook.AddRecipe(recipe);
			Console.WriteLine("Recipe added successfully!");
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

			// Read and validate category choice
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
			// Prompt user to enter the name of the recipe to remove
			Console.Write("Enter the name of the recipe to remove: ");
			string? recipeName = Console.ReadLine();
			if (recipeName == null)
			{
				Console.WriteLine("Recipe name cannot be null.");
				return;
			}

			// Remove the recipe from the recipe book
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
