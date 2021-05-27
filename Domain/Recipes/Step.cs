using System.ComponentModel.DataAnnotations;

namespace Domain.Recipes
{
    public class Step
    {
        [Required]
        public int Index { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}