using System.Linq;
using FaunaDB.Client;
using FaunaDB.Query;
using FaunaDB.Types;
using static FaunaDB.Query.Language;

namespace FaunaRepository
{
    public abstract class FaunaDbRepositoryBase
    {
        protected readonly string Collection;
        protected readonly FaunaClient Client;

        protected FaunaDbRepositoryBase(string collection, FaunaClient client)
        {
            Collection = collection;
            Client = client;
        }
        
        private async void CreateOrGetCollection()
        {
            var result = await Client.Query(Paginate(Collections()));
            IResult<Value[]> data = result.At("data").To<Value[]>();

            if (data.isSuccess)
            {
                var values = data.Value;
                if (values.Any(x => x != Collection(Collection)))
                {
                    var v = await Client.Query(CreateCollection(Obj("name", Collection)));
                }
                
            }

        }
    }
}