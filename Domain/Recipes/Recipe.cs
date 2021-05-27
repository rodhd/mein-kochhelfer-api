using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Domain.Authors;
using Domain.Ingredients;

namespace Domain.Recipes
{
    public class Recipe
    {
        [Required, NotNull]
        public string Id { get; set; }
        
        [Required, NotNull]
        public string Title { get; set; }

        [Required, NotNull]
        public int Rating { get; set; }
        
        public string? PhotoUrl { get; set; }
        
        public Author Author { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
        
        public List<Step> Steps { get; set; }
    }
}