
namespace DeutschDeck.WebAPI.Emails
{
    public class MockedEmailDelivery(EmailDeliveryAdapterConfiguration configuration, SignupTemplateProvider provider) : IEmailDeliveryAdapter
    {
        public Task SendSignupResponse(string recipient, string password)
        {
            Console.WriteLine("IEmailDeliveryAdapter:: recipient:{1} password:{2}", recipient, password);
            return Task.CompletedTask;
        }
    }
}
