namespace DeutschDeck.WebAPI.Emails
{
    public interface IEmailDeliveryAdapter
    {
        public Task SendSignupResponse(string recipient, string password);
    }

    public record EmailDeliveryAdapterConfiguration(string token, string endpoint, string sender);
}
