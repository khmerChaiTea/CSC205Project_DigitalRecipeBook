﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		// Parameters:
		//   recipe: The recipe to be added
		public void AddRecipe(Recipe recipe)
		{
			Recipes.Add(recipe); // Add the recipe to the list
		}

		// Finds a recipe by its name
		// Parameters:
		//   recipeName: The name of the recipe to search for
		// Returns:
		//   The recipe with the specified name if found; otherwise, null
		public Recipe? FindRecipeByName(string recipeName)
		{
			return Recipes.FirstOrDefault(r => r.RecipeName.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
			// Search for the recipe by name, ignoring case, and return it if found
		}

		// Finds all recipes that belong to a specific category
		// Parameters:
		//   category: The category of recipes to search for
		// Returns:
		//   A list of recipes that match the specified category
		public List<Recipe> FindRecipeByCategory(RecipeType category)
		{
			return Recipes.Where(r => r.Category == category).ToList();
			// Filter recipes by the specified category and return the list
		}

		// Removes a recipe by its name from the recipe book
		// Parameters:
		//   recipeName: The name of the recipe to be removed
		public void RemoveRecipe(string recipeName)
		{
			var recipe = FindRecipeByName(recipeName);
			// Find the recipe by name
			if (recipe != null)
			{
				Recipes.Remove(recipe); // Remove the recipe from the list if found
			}
		}
	}
}
