using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecipeBook
{
	// Represents an ingredient used in a recipe
	public class Ingredient
	{
		// The name of the ingredient (e.g., "Flour", "Sugar")
		public string? Name { get; set; }

		// The quantity of the ingredient needed (e.g., "2 cups", "100g")
		public string? Quantity { get; set; }

		// Constructor to initialize an ingredient with its name and quantity
		// Parameters:
		//   name: The name of the ingredient
		//   quantity: The amount of the ingredient needed
		public Ingredient(string name, string quantity)
		{
			Name = name;			// Set the ingredient's name
			Quantity = quantity;    // Set the ingredient's quantity
		}
	}
}
