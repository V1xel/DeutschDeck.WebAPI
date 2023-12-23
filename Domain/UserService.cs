using DeutschDeck.WebAPI.Database.Repositories;

namespace DeutschDeck.WebAPI.Domain
{
    public class UserService(UserRepository userRepository)
    {
        public async void SignupUser(SignupUserValidatedArgs args)
        {
            var (name, email, password) = args;
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(name, email, hash);

            await userRepository.Save(user);
        }
    }
}
