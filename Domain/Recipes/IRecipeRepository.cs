using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Recipes
{
    public interface IRecipeRepository
    {
        public Task<Recipe> GetRecipe(string recipeId);

        public Task<Recipe> AddRecipe(Recipe recipe);

        public Task<Recipe> UpdateRecipe(string id, Recipe recipe);

        public Task DeleteRecipe(string recipeId);

        public Task<List<Recipe>> SearchRecipes(string searchTerm);

        public Task<List<Recipe>> GetAll();
    }
}