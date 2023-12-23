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

    public record SignupUserValidatedArgs(string name, string email, string password);

    public class UserValidator
    {
        public static SignupUserValidatedArgs SignupUserArgs(string name, string email, string password)
        {
            var nameValidator = new RegexValidator();
            if (!nameValidator.IsValid(name))
                throw new ValidationException("name", "has invalid format");

            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
                throw new ValidationException("email", "has invalid format");

            var passwordValidator = new RegexValidator();
            if (!passwordValidator.IsValid(password))
                throw new ValidationException("password", "has invalid format");

            return new SignupUserValidatedArgs(name, email, password);
        }
    }
}
