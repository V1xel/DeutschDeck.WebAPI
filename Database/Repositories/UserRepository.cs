using DeutschDeck.WebAPI.Database.Entities;
using DeutschDeck.WebAPI.Database.Repositories.Base;
using DeutschDeck.WebAPI.Domain;

namespace DeutschDeck.WebAPI.Database.Repositories
{
    public class UserRepository(DDContext context) : Repository<User, UserEntity>(context)
    {
    }
}
