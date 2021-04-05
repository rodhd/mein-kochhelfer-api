namespace Domain.Ingredients
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int? Amount { get; set; }
        public Unit? Unit { get; set; }
    }
}