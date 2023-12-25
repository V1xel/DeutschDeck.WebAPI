using RestSharp.Authenticators;
using RestSharp;
using GraphQLParser;
using Microsoft.Extensions.Configuration;

namespace DeutschDeck.WebAPI.Emails
{
    public class MailerSendAdapter(EmailDeliveryAdapterConfiguration configuration, SignupTemplateProvider provider) : IEmailDeliveryAdapter
    {
        record MailerSendRerson(string email);
        record MailerSendRequestBody(MailerSendRerson from, List<MailerSendRerson> to, string subject, string html);

        private RestClient _client = new RestClient(new RestClientOptions(configuration.endpoint)
        {
            Authenticator = new JwtAuthenticator(configuration.token)
        });

        public async Task SendSignupResponse(string recipient, string password)
        {
            var request = new RestRequest();
            request.AddJsonBody(new MailerSendRequestBody(
                new MailerSendRerson(configuration.sender), 
                [new MailerSendRerson(recipient)],
                provider.Subject, 
                provider.GetFilledTemplate(password)
                ));
            await _client.PostAsync(request);
        }
    }
}
