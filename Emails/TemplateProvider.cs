using Microsoft.Extensions.Configuration;

namespace DeutschDeck.WebAPI.Emails
{
    public record SignupTemplateProviderConfiguration(string templatePath, string subject);
    public class SignupTemplateProvider(SignupTemplateProviderConfiguration configuration)
    {
        public string GetFilledTemplate(string password) 
        {
            var template = File.ReadAllText(configuration.templatePath);
            return string.Format(template, password);
        }

        public string Subject { get { return configuration.subject; } }
    }
}
