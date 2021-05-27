using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Authors;
using Domain.Ingredients;
using Domain.Recipes;
using Google.Cloud.Firestore;

namespace FirestoreRepository
{
    public class FirestoreDbRecipeRepository : IRecipeRepository
    {
        private static string collection = "Recipes";
        private readonly FirestoreDb _db;

        public FirestoreDbRecipeRepository(FirestoreDb db)
        {
            _db = db;
        }

        public async Task<Recipe> GetRecipe(string recipeId)
        {
            DocumentReference docRef = _db.Collection(collection).Document(recipeId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Console.WriteLine("Document data for {0} document:", snapshot.Id);
                Recipe recipe = Map(snapshot.ConvertTo<FirestoreDbRecipe>());
                return recipe;
            }
            else
            {
                Console.WriteLine("Document {0} does not exist!", snapshot.Id);
            }

            return null;
        }

        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            DocumentReference docRef = _db.Collection(collection).Document();
            FirestoreDbRecipe firestoreDbRecipe = new FirestoreDbRecipe()
            {
                Id = docRef.Id,
                Title = recipe.Title,
                Rating = recipe.Rating,
                PhotoUrl = recipe.PhotoUrl,
                Author = new FirestoreDbAuthor()
                {
                    FirstName = recipe.Author.FirstName,
                    LastName = recipe.Author.LastName
                },
                Ingredients = recipe.Ingredients.Select(i => new FirestoreDbIngredient()
                {
                    Name = i.Name,
                    Amount = i.Amount,
                    Unit = (int) i.Unit!
                }).ToList(),
                Steps = recipe.Steps.Select(s => new FirestoreDbStep()
                        {Index = s.Index, Description = s.Description})
                    .ToList()
            };

            await docRef.CreateAsync(firestoreDbRecipe);

            return recipe;
        }

        public async Task<Recipe> UpdateRecipe(string id, Recipe recipe)
        {
            DocumentReference docRef = _db.Collection(collection).Document(id);
            await docRef.SetAsync(new FirestoreDbRecipe()
            {
                Id = docRef.Id,
                Title = recipe.Title,
                Rating = recipe.Rating,
                PhotoUrl = recipe.PhotoUrl,
                Author = new FirestoreDbAuthor()
                {
                    FirstName = recipe.Author.FirstName,
                    LastName = recipe.Author.LastName
                },
                Ingredients = recipe.Ingredients.Select(i => new FirestoreDbIngredient()
                {
                    Name = i.Name,
                    Amount = i.Amount,
                    Unit = (int) i.Unit!
                }).ToList(),
                Steps = recipe.Steps.Select(s => new FirestoreDbStep()
                        {Index = s.Index, Description = s.Description})
                    .ToList()
            });
            return recipe;
        }

        public async Task DeleteRecipe(string recipeId)
        {
            DocumentReference docRef = _db.Collection(collection).Document(recipeId);
            await docRef.DeleteAsync();
        }

        public async Task<List<Recipe>> SearchRecipes(string searchTerm)
        {
            var recipes = await GetAll();
            return recipes
                .Where(r => r.Title.ToLower().Contains(searchTerm.ToLower()))
                .ToList();
        }

        public async Task<List<Recipe>> GetAll()
        {
            Query allRecipesQuery = _db.Collection(collection);
            QuerySnapshot allRecipesSnapshot = await allRecipesQuery.GetSnapshotAsync();
            return allRecipesSnapshot
                .Documents
                .Select(r => Map(r.ConvertTo<FirestoreDbRecipe>()))
                .ToList();
        }

        private static Recipe Map(FirestoreDbRecipe recipe)
        {
            return new Recipe()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                Rating = recipe.Rating,
                PhotoUrl = recipe.PhotoUrl,
                Author = new Author()
                {
                    FirstName = recipe.Author.FirstName,
                    LastName = recipe.Author.LastName
                },
                Ingredients = recipe.Ingredients.Select(i => new Ingredient()
                {
                    Name = i.Name,
                    Amount = i.Amount,
                    Unit = (Unit) i.Unit!
                }).ToList(),
                Steps = recipe.Steps.Select(s => new Step()
                        {Index = s.Index, Description = s.Description})
                    .ToList()
            };
        }
    }
}