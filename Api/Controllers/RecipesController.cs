using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Domain.Recipes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("recipes")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipesController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetAllRecipes()
        {
            var result = await _recipeRepository.GetAll();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeById(string id)
        {
            var result = await _recipeRepository.GetRecipe(id);
            return Ok(result);
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<List<Recipe>>> SearchRecipes([FromRoute] string searchTerm)
        {
            var result = await _recipeRepository.SearchRecipes(searchTerm);
            return Ok(result);
        }
        

        [HttpPost]
        public async Task<ActionResult<Recipe>> AddRecipe([FromBody] CreateRecipeModel model)
        {
            var result = await _recipeRepository.AddRecipe(model.ToEntity());
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> UpdateRecipe([FromRoute] string id,[FromBody] Recipe recipe)
        {
            var result = await _recipeRepository.UpdateRecipe(id, recipe);
            return Ok(recipe);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe(string id)
        {
            await _recipeRepository.DeleteRecipe(id);
            return Ok("Deleted");
        }
    }
}