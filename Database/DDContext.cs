using Microsoft.EntityFrameworkCore;

namespace DeutschDeck.WebAPI.Database
{
    public class DDContext(DbContextOptions<DDContext> options) : DbContext(options)
    {
    }
}
