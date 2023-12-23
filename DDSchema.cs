using GraphQL.Types;

namespace DeutschDeck.WebAPI
{
    public class DDSchema : Schema
    {
        public DDSchema(IServiceProvider provider) : base(provider)
        {
        }
    }
}
