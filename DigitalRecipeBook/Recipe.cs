using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		// This property can be null if no instructions are provided
		public string? Instructions { get; set; }

		// The category or type of recipe (e.g., Dessert, Main Course)
		public RecipeType Category { get; set; }

		// Constructor to initialize a recipe with its name and category
		// Parameters:
		//   name: The name of the recipe (e.g., "Chocolate Cake")
		//   category: The type or category of the recipe (e.g., Dessert, Main Course)
		public Recipe(string name, RecipeType category)
		{
			RecipeName = name;                      // Set the name of the recipe
			Category = category;                    // Set the category of the recipe
			Ingredients = new List<Ingredient>();   // Initialize the list of ingredients to an empty list
		}

		// Adds a new ingredient to the recipe
		// Parameters:
		//   ingredient: The ingredient to be added to the recipe
		public void AddIngredient(Ingredient ingredient)
		{
			Ingredients.Add(ingredient); // Add the ingredient to the list of ingredients
		}

		// Updates the cooking instructions for the recipe
		// Parameters:
		//   newInstructions: The updated instructions for preparing the recipe
		public void UpdateInstructions(string newInstructions)
		{
			Instructions = newInstructions; // Set the new instructions
		}
	}
}

