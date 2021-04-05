using System.Collections.Generic;
using Domain.Authors;
using Domain.Ingredients;

namespace Domain.Recipes
{
    public class Recipe
    {
        public string Id { get; set; }
        
        public string Title { get; set; }

        public int Rating { get; set; }
        
        public string? PhotoUrl { get; set; }
        
        public Author Author { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
        
        public List<Step> Steps { get; set; }
    }
}