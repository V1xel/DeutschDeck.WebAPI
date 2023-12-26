using GraphQL.Types;

namespace DeutschDeck.WebAPI.Graphql
{
    public class DDSchema : Schema
    {
        public DDSchema(IServiceProvider provider) : base(provider)
        {
        }
    }
}
