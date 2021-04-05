using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Authors;
using Domain.Ingredients;
using Domain.Recipes;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;
using static FaunaDB.Query.Language;


namespace FaunaRepository.Recipes
{
    public class FaunaDbRecipeRepository: FaunaDbRepositoryBase ,IRecipeRepository
    {
        public FaunaDbRecipeRepository(FaunaClient client) : base("recipes", client)
        {
            
        }

        public async Task<Recipe> GetRecipe(string recipeId)
        {
            Value result = await Client.Query(
                Map(
                    Paginate(Match(Index("recipe_id"), recipeId)),
                    Lambda("recipe", Get(Var("recipe")))
                )
                );

            var data = result.At("data").At(0);

            var recipe = DecodeRecipe(data);

            return recipe;
        }

        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            Expr data = EncodeRecipe(recipe);
            
            Value result = await Client.Query(
                Create(
                    Collection(Collection),
                    Obj("data", data
                    )
                )
            );

            return recipe;
        }

        public async Task<Recipe> UpdateRecipe(string recipeId, Recipe recipe)
        {
            Expr data = EncodeRecipe(recipe);

            Value result = await Client.Query(
            Update(
                Select(0,Select("data",Paginate(Match(Index("recipe_id"), recipeId)))),
                Obj("data", data)
                    ));

            return recipe;
        }

        public async Task DeleteRecipe(string recipeId)
        {
            Value result = await Client.Query(
                Delete(
                    Select(0,Select("data",Paginate(Match(Index("recipe_id"), recipeId))))
                ));
        }

        public async Task<List<Recipe>> SearchRecipes(string searchTerm)
        {
            Value result = await Client.Query(
                Map(
                    Filter(
                        Paginate(
                            Match(
                                Index("recipes_4"))), x => ContainsStr(Casefold(Select(1, x)), searchTerm)),
                     x => Get(Select(0, x)))
            );

            var data = result.At("data").To<Value[]>();

            var recipes = data.Value.Select(DecodeRecipe).ToList();

            return recipes;
        }

        public async Task<List<Recipe>> GetAll()
        {
            Value result = await Client.Query(
                Map(
                        Paginate(
                            Match(
                                Index("recipes"))),
                    x => Get(x))
            );
            
            var data = result.At("data").To<Value[]>();

            var recipes = data.Value.Select(DecodeRecipe).ToList();

            return recipes;
        }


        private Recipe DecodeRecipe(Value data)
        {
            List<Ingredient> ingredients = data
                .At("data")
                .At("ingredients")
                .To<Value[]>()
                .Value
                .Select(x => new Ingredient()
                    {Amount = (int) x.At("amount"), Name = (string) x.At("name"), Unit = (Unit) (int) x.At("unit")})
                .ToList();
            
            List<Step> steps = data
                .At("data")
                .At("steps")
                .To<Value[]>()
                .Value
                .Select(x => new Step()
                    {Index = (int) x.At("index"), Description = (string) x.At("description")})
                .ToList();

            Author author = new Author()
            {
                FirstName = (string) data.At("data").At("author").At("first_name"),
                LastName = (string) data.At("data").At("author").At("last_name")
            };
            
            return new Recipe()
            {
                Id = (string) data.At("data").At("id"),
                Title = (string) data.At("data").At("title"),
                Rating = (int) data.At("data").At("rating"),
                PhotoUrl = data.At("data").At("photoUrl") != NullV.Instance ? (string) data.At("data").At("photoUrl") : null,
                Author = author,
                Ingredients = ingredients,
                Steps = steps
            };
        }

        private static Expr EncodeRecipe(Recipe recipe)
        {
            var author = Obj(
                "first_name", recipe.Author.FirstName,
                "last_name", recipe.Author.LastName);

            var ingredients = Arr(
                recipe.Ingredients.Select(
                    x => Obj(
                        "name", x.Name,
                        "amount", x.Amount,
                        "unit", x.Unit != null ? (int) x.Unit : null)
                )
            );

            var steps = Arr(
                recipe.Steps.Select(
                    x => Obj(
                        "index", x.Index, 
                        "description", x.Description)
                )
            );

            Expr expr = Obj(new Dictionary<string, Expr>()
            {
                {"id", recipe.Id},
                {"title", recipe.Title},
                {"rating", recipe.Rating},
                {"photo_url", recipe.PhotoUrl},
                {"author", author},
                {"ingredients", ingredients},
                {"steps", steps}
            });

            return expr;
        }

    }
}