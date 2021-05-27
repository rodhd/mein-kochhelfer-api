using System.Collections.Generic;
using Google.Cloud.Firestore;

namespace FirestoreRepository
{
    [FirestoreData]
    public sealed class FirestoreDbRecipe
    {
        [FirestoreDocumentId]
        public string Id { get; set; }

        [FirestoreProperty]
        public string Title { get; set; }
        
        [FirestoreProperty]
        public int Rating { get; set; }
        
        [FirestoreProperty]
        public string? PhotoUrl { get; set; }
        
        [FirestoreProperty]
        public FirestoreDbAuthor Author { get; set; }
        
        [FirestoreProperty]
        public List<FirestoreDbIngredient>  Ingredients { get; set; }
        
        [FirestoreProperty]
        public List<FirestoreDbStep> Steps { get; set; }
        
        
    }

    [FirestoreData]
    public sealed class FirestoreDbAuthor
    {
        [FirestoreProperty]
        public string FirstName { get; set; }
        
        [FirestoreProperty] 
        public string LastName { get; set; }
    }

    [FirestoreData]
    public sealed class FirestoreDbIngredient
    {
        [FirestoreProperty]
        public string Name { get; set; }
        
        [FirestoreProperty]
        public int? Amount { get; set; }
        
        [FirestoreProperty]
        public int? Unit { get; set; }
    }

    [FirestoreData]
    public sealed class FirestoreDbStep
    {
        [FirestoreProperty]
        public int Index { get; set; }
        
        [FirestoreProperty]
        public string Description { get; set; }
    }
}