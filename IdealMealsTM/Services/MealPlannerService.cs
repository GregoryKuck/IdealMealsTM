using IdealMealsTM.Models;

namespace IdealMealsTM.Services
{
    // HACK: This should be recreated as injectable, but I am not using DI for this sample
    // NOTE: This can probably be further optimised using hashsets and unions, but this should be good enough for the assessment
    public class MealPlannerService
    {
        private List<Recipe> _recipes;
        private Dictionary<string, Ingredient> _availableIngredients;
        private int _maxMeals;
        private List<List<Recipe>> _optimalCombinations;
        private List<Recipe> _currentCombination;
        private int _currentPeopleFed;

        public MealPlannerService(List<Recipe> recipes, List<Ingredient> ingredients)
        {
            _recipes = recipes;
            _availableIngredients = ingredients.ToDictionary(i => i.Name, i => i);
            _maxMeals = 0;
            _optimalCombinations = new List<List<Recipe>>();
            _currentCombination = new List<Recipe>();
        }

        public void FindOptimalCombinations()
        {
            ExploreCombinations(0);
            PrintOptimalCombinations();
        }

        private void ExploreCombinations(int recipeIndex)
        {
            if (_currentPeopleFed > _maxMeals)
            {
                _maxMeals = _currentPeopleFed;
                _optimalCombinations.Clear();
                _optimalCombinations.Add(new List<Recipe>(_currentCombination));
            }
            else if (_currentPeopleFed == _maxMeals)
            {
                _optimalCombinations.Add(new List<Recipe>(_currentCombination));
            }

            for (int i = recipeIndex; i < _recipes.Count; i++)
            {
                if (CanAddRecipe(_recipes[i]))
                {
                    AddRecipeToCombination(_recipes[i]);
                    ExploreCombinations(i);
                    RemoveRecipeFromCombination(_recipes[i]);
                }
            }
        }

        private bool CanAddRecipe(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!_availableIngredients.TryGetValue(ingredient.Key, out Ingredient availableIngredient) || availableIngredient.Quantity < ingredient.Value)
                {
                    return false;
                }
            }
            return true;
        }

        private void AddRecipeToCombination(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                _availableIngredients[ingredient.Key].Quantity -= ingredient.Value;
            }
            _currentCombination.Add(recipe);
            _currentPeopleFed += recipe.MealsProduced;
        }

        private void RemoveRecipeFromCombination(Recipe recipe)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                _availableIngredients[ingredient.Key].Quantity += ingredient.Value;
            }
            _currentCombination.Remove(recipe);
            _currentPeopleFed -= recipe.MealsProduced;
        }

        private void PrintOptimalCombinations()
        {
            Console.WriteLine($"Maximum Possible Meals: {_maxMeals}");
            Console.WriteLine("Optimal Combinations:");

            int combinationIndex = 1;
            foreach (var combination in _optimalCombinations)
            {
                Console.WriteLine($"Combination {combinationIndex++}: {string.Join(", ", combination.Select(r => r.Name))}");
                Console.WriteLine($"People Fed: {_maxMeals}");

                // Calculate remaining ingredients
                Dictionary<string, int> remainingIngredients = _availableIngredients.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Quantity);

                foreach (var recipe in combination)
                {
                    foreach (var ingredient in recipe.Ingredients)
                    {
                        remainingIngredients[ingredient.Key] -= ingredient.Value;
                    }
                }

                Console.WriteLine("Ingredients Leftover:");
                foreach (var kvp in remainingIngredients)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

                Console.WriteLine();
            }
        }
    }
}
