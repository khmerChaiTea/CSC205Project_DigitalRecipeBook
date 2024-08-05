using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecipeBook
{
	// One of the core classes for this project
	public class Recipe
	{
		public string? RecipeName { get; set; }
		public List<Ingredient> Ingredients {  get; set; }
		public string? Instructions { get; set; }
		public RecipeType Category { get; set; }

		public Recipe(string name, RecipeType category)
		{
			RecipeName = name;
			Category = category;
			Ingredients = new List<Ingredient>();
		}

		public void AddIngredient(Ingredient ingredient)
		{
			Ingredients.Add(ingredient);
		}

		public void UpdateInstructions(string newInstructions)
		{
			Instructions = newInstructions;
		}
	}
}
