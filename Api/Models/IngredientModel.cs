using System;
using Domain.Ingredients;

namespace Api.Models
{
    public class IngredientModel
    {
        public string Name { get; set; }
        public int? Amount { get; set; }
        public Unit? Unit { get; set; }

        public Ingredient ToEntity()
        {
            return new Ingredient()
            {
                Name = this.Name,
                Amount = this.Amount,
                Unit = this.Unit
            };
        }
    }
}