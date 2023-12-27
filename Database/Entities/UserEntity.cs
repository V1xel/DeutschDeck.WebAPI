using System.ComponentModel.DataAnnotations.Schema;

namespace DeutschDeck.WebAPI.Database.Entities
{
    public class UserEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }
    }
}
