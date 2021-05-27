using System.ComponentModel.DataAnnotations;

namespace Domain.Ingredients
{
    public class Ingredient
    {
        [Required]
        public string Name { get; set; }
        public int? Amount { get; set; }
        public Unit? Unit { get; set; }
    }
}