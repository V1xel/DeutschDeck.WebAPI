using System.ComponentModel.DataAnnotations;

namespace DeutschDeck.WebAPI.Domain
{
    public class ValidationException : Exception
    {
        public ValidationException(string source, string message) : base(message)
        {
            Source = source;
        }
    }

    public class RegexValidator
    {
        public bool IsValid(string value)
        {
            return true;
        }
    }

    public record SignupValidatedArgs(string email);

    public class UserValidator
    {
        public static SignupValidatedArgs SignupArgs(string email)
        {
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
                throw new ValidationException("email", "has invalid format");

            return new SignupValidatedArgs(email);
        }
    }
}
