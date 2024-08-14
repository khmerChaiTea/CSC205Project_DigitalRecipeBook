namespace DigitalRecipeBook
{
    // Represents a recipe in the digital recipe book
    public class Recipe
	{
		private string recipeName = string.Empty;

		// The name of the recipe (e.g., "Chocolate Cake")
		public string RecipeName
		{
			get => recipeName;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentNullException(nameof(value), "Recipe name cannot be null or empty.");
				recipeName = value;
			}
		}

		// A list of ingredients required for the recipe
		public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

		// Cooking instructions for preparing the recipe (nullable)
		public string? Instructions { get; set; } = null;

		// The category or type of recipe (e.g., Dessert, Main Course)
		public RecipeType Category { get; set; }

		// Default constructor for JSON deserialization
		public Recipe()
		{
			// Optionally, initialize default values
		}

		// Constructor to initialize a recipe with its name and category
		public Recipe(string name, RecipeType category)
		{
			RecipeName = name ?? throw new ArgumentNullException(nameof(name), "Recipe name cannot be null."); // Ensure name is not null
			Category = category; // Set the category of the recipe
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
