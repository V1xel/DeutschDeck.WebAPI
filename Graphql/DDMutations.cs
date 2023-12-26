using DeutschDeck.WebAPI.Domain;
using DeutschDeck.WebAPI.Utilities;
using GraphQL;
using GraphQL.Types;

namespace DeutschDeck.WebAPI.Graphql
{
    public class DDQueries : ObjectGraphType
    {
        public DDQueries() 
        {
            Field<BooleanGraphType>("test").Resolve(c => true);
        }
    }

    public class DDMutations : ObjectGraphType
    {
        public const string EMAIL_FIELD_LITERAL = "email";

        public DDMutations(UserService userService)
        {
            Field<BooleanGraphType>("signup")
                .Arguments(new QueryArguments(
                    new QueryArgument<StringGraphType> { Name = EMAIL_FIELD_LITERAL }
                ))
                .Resolve(context =>
                {
                    var email = context.GetArgument<string>(EMAIL_FIELD_LITERAL);

                    userService.Signup(new SignupValidatedArgs(email));

                    return true;
                });
        }
    }
}
