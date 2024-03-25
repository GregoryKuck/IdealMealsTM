namespace IdealMealsTM.Models
{
    public class Recipe
    {
        public string? Name { get; set; }
        public Dictionary<string, int> Ingredients { get; set; }
        public int MealsProduced { get; set; }
    }
}
