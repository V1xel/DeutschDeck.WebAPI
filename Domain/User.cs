using Microsoft.AspNetCore.Identity;

namespace DeutschDeck.WebAPI.Domain
{
    public class User
    {
        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
