using Microsoft.AspNetCore.Identity;

namespace DeutschDeck.WebAPI.Domain
{
    public class User
    {
        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
