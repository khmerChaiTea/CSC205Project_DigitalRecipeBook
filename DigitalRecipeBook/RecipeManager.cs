using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DigitalRecipeBook
{
	// Provides methods for saving and loading recipe data to and from a file
	public static class RecipeManager
	{
		// Saves the recipe book data to a file in JSON format
		// Parameters:
		//   filePath: The path of the file where the recipe data will be saved
		//   recipeBook: The RecipeBook object to be saved
		public static void SaveRecipeData(string filePath, RecipeBook recipeBook)
		{
			Console.WriteLine("Saving recipe data...");
			try
			{
				// Serialize the RecipeBook object to a JSON string with indented formatting
				var json = JsonConvert.SerializeObject(recipeBook, Formatting.Indented);

				// Write the JSON string to the specified file path
				File.WriteAllText(filePath, json);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error saving recipe data: {ex.Message}");
			}
		}

		// Loads recipe book data from a file in JSON format
		// Parameters:
		//   filePath: The path of the file from which the recipe data will be loaded
		// Returns:
		//   A RecipeBook object populated with the data from the file, or a new RecipeBook if the file does not exist
		public static RecipeBook? LoadRecipeData(string filePath)
		{
			// Check if the file exists
			if (!File.Exists(filePath))
			{
				// Return a new RecipeBook if the file does not exist
				return new RecipeBook();
			}

			// Read the JSON string from the specified file path
			var json = File.ReadAllText(filePath);

			// Deserialize the JSON string to a RecipeBook object and return it
			// Handle null return value
			var recipeBook = JsonConvert.DeserializeObject<RecipeBook>(json);
			return recipeBook ?? new RecipeBook();
		}
	}
}