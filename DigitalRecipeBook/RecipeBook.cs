using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalRecipeBook
{
	public class RecipeBook
	{
		public List<Recipe> Recipes { get; set; }

		public RecipeBook()
		{
			Recipes = new List<Recipe>();
		}
	}
}
