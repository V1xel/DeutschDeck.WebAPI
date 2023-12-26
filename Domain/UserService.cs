using DeutschDeck.WebAPI.Database.Repositories;
using DeutschDeck.WebAPI.Emails;
using DeutschDeck.WebAPI.Utilities;
using PasswordGenerator;

namespace DeutschDeck.WebAPI.Domain
{
    public class UserService(UserRepository userRepository, IEmailDeliveryAdapter emailDelivery, PasswordGenerationUtility passwordGeneration)
    {
        public async void Signup(SignupValidatedArgs args)
        {
            var password = passwordGeneration.Generate();
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(args.email, hash);

            await userRepository.Save(user);

            await emailDelivery.SendSignupResponse(args.email, password);
        }
    }
}
