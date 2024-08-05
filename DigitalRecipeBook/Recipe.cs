using System;
using System.Collections.Generic;

namespace DigitalRecipeBook
{
	// Represents a recipe in the digital recipe book
	public class Recipe
	{
		// The name of the recipe (e.g., "Chocolate Cake", "Spaghetti Carbonara")
		public string RecipeName { get; set; }

		// A list of ingredients required for the recipe
		public List<Ingredient> Ingredients { get; set; }

		// Cooking instructions for preparing the recipe (nullable)
		public string? Instructions { get; set; }

		// The category or type of recipe (e.g., Dessert, Main Course)
		public RecipeType Category { get; set; }

		// Constructor to initialize a recipe with its name and category
		public Recipe(string name, RecipeType category)
		{
			RecipeName = name ?? throw new ArgumentNullException(nameof(name), "Recipe name cannot be null."); // Ensure name is not null
			Category = category; // Set the category of the recipe
			Ingredients = new List<Ingredient>(); // Initialize the list of ingredients to an empty list
		}

		// Adds a new ingredient to the recipe
		public void AddIngredient(Ingredient? ingredient)
		{
			if (ingredient == null)
			{
				Console.WriteLine("Cannot add a null ingredient.");
				return; // Do nothing if ingredient is null
			}
			Ingredients.Add(ingredient); // Add the ingredient to the list of ingredients
		}

		// Updates the cooking instructions for the recipe
		public void UpdateInstructions(string newInstructions)
		{
			Instructions = newInstructions; // Set the new instructions
		}
	}
}
