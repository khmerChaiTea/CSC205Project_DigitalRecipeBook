using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalRecipeBook;
using System;
using System.Collections.Generic;

namespace DigitalRecipeBook.Tests
{
	[TestClass()]
	public class RecipeTests
	{
		[TestMethod()]
		public void AddIngredient_IngredientIsNotNull_ShouldAddIngredient()
		{
			// Arrange
			var recipe = new Recipe("Pancakes", RecipeType.MainCourse);
			var ingredient = new Ingredient("Flour", "2 cups");

			// Act
			recipe.AddIngredient(ingredient);

			// Assert
			Assert.AreEqual(1, recipe.Ingredients.Count);
			Assert.AreEqual("Flour", recipe.Ingredients[0].Name);
			Assert.AreEqual("2 cups", recipe.Ingredients[0].Quantity);
		}

		[TestMethod()]
		public void ListRecipes_WhenRecipesAdded_ShouldReturnAllRecipes()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe1 = new Recipe("Pancakes", RecipeType.MainCourse);
			var recipe2 = new Recipe("Salad", RecipeType.Appetizer);
			recipeBook.AddRecipe(recipe1);
			recipeBook.AddRecipe(recipe2);

			// Act
			var recipes = recipeBook.Recipes;

			// Assert
			Assert.AreEqual(2, recipes.Count);
			Assert.AreEqual("Pancakes", recipes[0].RecipeName);
			Assert.AreEqual("Salad", recipes[1].RecipeName);
		}

		[TestMethod()]
		public void ListRecipes_WhenNoRecipes_ShouldReturnEmptyList()
		{
			// Arrange
			var recipeBook = new RecipeBook();

			// Act
			var recipes = recipeBook.Recipes;

			// Assert
			Assert.AreEqual(0, recipes.Count);
		}

		[TestMethod()]
		public void FindRecipeByName_RecipeExists_ShouldReturnRecipe()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe = new Recipe("Pancakes", RecipeType.MainCourse);
			recipeBook.AddRecipe(recipe);

			// Act
			var foundRecipe = recipeBook.FindRecipeByName("Pancakes");

			// Assert
			Assert.IsNotNull(foundRecipe);
			Assert.AreEqual("Pancakes", foundRecipe.RecipeName);
		}

		[TestMethod()]
		public void FindRecipeByName_RecipeDoesNotExist_ShouldReturnNull()
		{
			// Arrange
			var recipeBook = new RecipeBook();

			// Act
			var foundRecipe = recipeBook.FindRecipeByName("Pancakes");

			// Assert
			Assert.IsNull(foundRecipe);
		}

		[TestMethod()]
		public void RemoveRecipe_RecipeExists_ShouldRemoveRecipe()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe = new Recipe("Pancakes", RecipeType.MainCourse);
			recipeBook.AddRecipe(recipe);

			// Act
			recipeBook.RemoveRecipe("Pancakes");

			// Assert
			Assert.AreEqual(0, recipeBook.Recipes.Count);
		}

		[TestMethod()]
		public void RemoveRecipe_RecipeDoesNotExist_ShouldNotChangeList()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe = new Recipe("Pancakes", RecipeType.MainCourse);
			recipeBook.AddRecipe(recipe);

			// Act
			recipeBook.RemoveRecipe("NonExistingRecipe");

			// Assert
			Assert.AreEqual(1, recipeBook.Recipes.Count);
			Assert.AreEqual("Pancakes", recipeBook.Recipes[0].RecipeName);
		}

		[TestMethod()]
		public void AddRecipe_DuplicateRecipe_ShouldNotAddDuplicate()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe1 = new Recipe("Pancakes", RecipeType.MainCourse);
			var recipe2 = new Recipe("Pancakes", RecipeType.MainCourse); // Duplicate

			// Act
			recipeBook.AddRecipe(recipe1);
			recipeBook.AddRecipe(recipe2);

			// Assert
			Assert.AreEqual(1, recipeBook.Recipes.Count); // Assuming duplicates are not allowed
			Assert.AreEqual("Pancakes", recipeBook.Recipes[0].RecipeName);
		}

		[TestMethod()]
		public void AddRecipe_RecipeWithEmptyName_ShouldNotAddRecipe()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe = new Recipe(string.Empty, RecipeType.MainCourse);

			// Act
			recipeBook.AddRecipe(recipe);

			// Assert
			Assert.AreEqual(0, recipeBook.Recipes.Count); // Assuming empty names are not allowed
		}

		[TestMethod()]
		public void AddRecipe_RecipeWithNullIngredients_ShouldHandleGracefully()
		{
			// Arrange
			var recipeBook = new RecipeBook();
			var recipe = new Recipe("Pancakes", RecipeType.MainCourse);

			// Act
			// Attempt to add a null ingredient
			recipe.AddIngredient(null);

			// Add the recipe to the recipe book
			recipeBook.AddRecipe(recipe);

			// Assert
			// Verify that the recipe was added and that it contains no ingredients
			Assert.AreEqual(0, recipe.Ingredients.Count);
			Assert.IsNotNull(recipeBook.FindRecipeByName("Pancakes"));
		}
	}
}
