#nullable enable
using System;
using System.Collections.Generic;
using Domain.Authors;
using Domain.Ingredients;
using Domain.Recipes;

namespace Api.Models
{
    public class CreateRecipeModel
    {
        public string Title { get; set; }

        public int Rating { get; set; }
        
        public string? PhotoUrl { get; set; }
        
        public Author Author { get; set; }
        
        public List<Ingredient> Ingredients { get; set; }
        
        public List<Step> Steps { get; set; }

        public Recipe ToEntity()
        {
            return new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Title = this.Title,
                Rating = this.Rating,
                PhotoUrl = this.PhotoUrl,
                Author = this.Author,
                Ingredients = this.Ingredients,
                Steps = this.Steps
            };
        }
    }
}