namespace DigitalRecipeBook
{
    // Represents an ingredient used in a recipe
    public class Ingredient
    {
        // The name of the ingredient (e.g., "Flour", "Sugar")
        public string Name { get; set; }

        // The quantity of the ingredient needed (e.g., "2 cups", "100g")
        public string Quantity { get; set; }

        // Constructor to initialize an ingredient with its name and quantity
        public Ingredient(string name, string quantity)
        {
            Name = name;         // Set the ingredient name
            Quantity = quantity; // Set the ingredient quantity
        }
    }
}
