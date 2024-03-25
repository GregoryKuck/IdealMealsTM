using IdealMealsTM.Models;
using IdealMealsTM.Services;

namespace IdealMealsTM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HACK: These should be added via a datasource (SQL, JSON, etc.), have hardcoded for now
            var ingredients = new List<Ingredient>
        {
            new Ingredient { Name = "Cucumber", Quantity = 2 },
            new Ingredient { Name = "Olives", Quantity = 2 },
            new Ingredient { Name = "Lettuce", Quantity = 3 },
            new Ingredient { Name = "Meat", Quantity = 6 },
            new Ingredient { Name = "Tomato", Quantity = 6 },
            new Ingredient { Name = "Cheese", Quantity = 8 },
            new Ingredient { Name = "Dough", Quantity = 10 }
        };

            var recipes = new List<Recipe>
        {
            new Recipe
            {
                Name = "Burger",
                Ingredients = new Dictionary<string, int>
                {
                    { "Meat", 1 },
                    { "Lettuce", 1 },
                    { "Tomato", 1 },
                    { "Cheese", 1 },
                    { "Dough", 1 }
                },
                MealsProduced = 1
            },
            new Recipe
            {
                Name = "Pie",
                Ingredients = new Dictionary<string, int>
                {
                    { "Meat", 2 },
                    { "Dough", 2 }
                },
                MealsProduced = 1
            },
            new Recipe
            {
                Name = "Sandwich",
                Ingredients = new Dictionary<string, int>
                {
                    { "Cucumber", 1 },
                    { "Dough", 1 }
                },
                MealsProduced = 1
            },
            new Recipe
            {
                Name = "Pasta",
                Ingredients = new Dictionary<string, int>
                {
                    { "Dough", 2 },
                    { "Tomato", 1 },
                    { "Cheese", 2 },
                    { "Meat", 1 },
                },
                MealsProduced = 2
            },
            new Recipe
            {
                Name = "Salad",
                Ingredients = new Dictionary<string, int>
                {
                    { "Lettuce", 2 },
                    { "Tomato", 2 },
                    { "Cucumber", 1 },
                    { "Cheese", 2 },
                    { "Olives", 1 }
                },
                MealsProduced = 3
            },
            new Recipe
            {
                Name = "Pizza",
                Ingredients = new Dictionary<string, int>
                {
                    { "Dough", 3 },
                    { "Tomato", 2 },
                    { "Cheese", 3 },
                    { "Olives", 1 }
                },
                MealsProduced = 4
            }
        };

            MealPlannerService planner = new MealPlannerService(recipes, ingredients);
            planner.FindOptimalCombinations();
        }
    }
}
