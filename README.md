# CSC205FinalProject

Below is my outline.

### **Digital Recipe Book Project Overview**

#### **1. Project Structure**

**Classes:**
- `Recipe`
- `Ingredient`
- `RecipeBook`
- `RecipeManager` (for managing file operations and random recipe suggestions)

**Enums:**
- `RecipeType` (`Category` for different types of recipes, e.g., Appetizer, Main Course, Dessert ) 

#### **2. Class Details**

**`Recipe` Class:**
- **Properties:**
  - `RecipeName`: Name of the recipe (string)
  - `Ingredients`: List of `Ingredient` objects
  - `Instructions`: Preparation steps (string)
  - `Category`: Category of the recipe (enum `RecipeType`)

- **Methods:**
  - `AddIngredient(Ingredient ingredient)`: Adds an ingredient to the recipe
  - `UpdateInstructions(string newInstructions)`: Updates the preparation steps

**`Ingredient` Class:**
- **Properties:**
  - `Name`: Name of the ingredient (string)
  - `Quantity`: Quantity of the ingredient (string)

- **Constructor:**
  - `Ingredient(string name, string quantity)`

**`Category` Enum:**
- Values: `Appetizer`, `MainCourse`, `Dessert`

**`RecipeBook` Class:**
- **Properties:**
  - `Recipes`: List of `Recipe` objects

- **Methods:**
  - `AddRecipe(Recipe recipe)`: Adds a new recipe to the book
  - `FindRecipeByCategory(RecipeType type)`: Finds recipes by category
  - `RemoveRecipe(string recipeName)`: Removes a recipe by its name

**`RecipeManager` Class:**
- **Methods:**
  - `LoadRecipeData(string filePath)`: Loads recipes from a file
  - `SaveRecipeData(string filePath)`: Saves recipes to a file

- **Methods:**
  - `GetCategory()`: Returns the category of the recipe

#### **3. Implementation Plan**

**Step 1: Define the Classes and Enums**

Create the basic structure for the `Recipe`, `Ingredient`, and `RecipeBook` classes, along with the `RecipeType` enum and `ICategorizable` interface.

**Step 2: Implement Methods in Classes**

Develop methods for managing recipes, such as adding, updating, and finding recipes. Implement methods for loading and saving recipe data.

**Step 3: Add File I/O Functionality**

Implement file reading and writing functionality to save and load recipes. Use JSON or XML format for easy parsing.

**Step 4: Implement Random Recipe Suggestion**

Use a random number generator to suggest a random recipe from the `RecipeBook`.

**Step 5: Develop the Graphical Interface (Optional)**

Create a GUI using WPF or Windows Forms to manage recipes visually. Include features for adding, editing, and viewing recipes.

**Step 6: Integrate with Database (Optional)**

If including a database, design a schema for storing recipes and integrate SQL operations into the `RecipeManager` class.

**Step 7: Version Control**

// Example Usage

public class Program

{

    public static void Main()
    
    {
        // Create a recipe
        var pancakeRecipe = new Recipe("Pancakes", RecipeType.MainCourse);
        pancakeRecipe.AddIngredient(new Ingredient("Flour", "2 cups"));
        pancakeRecipe.AddIngredient(new Ingredient("Milk", "1.5 cups"));
        pancakeRecipe.UpdateInstructions("Mix ingredients and cook on a skillet.");

        // Create a recipe book and add the recipe
        var recipeBook = new RecipeBook();
        recipeBook.AddRecipe(pancakeRecipe);

        // Save to file
        RecipeManager.SaveRecipeData("recipes.json", recipeBook);

        // Load from file
        var loadedRecipeBook = RecipeManager.LoadRecipeData("recipes.json");

        // Suggest a random recipe
        var randomRecipe = RecipeManager.SuggestRandomRecipe(loadedRecipeBook);
        Console.WriteLine($"Suggested Recipe: {randomRecipe.RecipeName}");
    }
}

Using Visual Studio 2022, after Cloning this Repo., use CTRL + F5 to run the App.
The console will Display a loading page with Options. This app use Json file to recode or store the recipe after it is saved. The Json file is located in the ..DigitalRecipeBook\bin\Debug\net8.0\. If you want to save the Json with you recipes, you can manually added to GitHub and manually added put them back in the bin directory. Feel free to give suggestion if you think you can add kool feature to this project.
