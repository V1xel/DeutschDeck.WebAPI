using PasswordGenerator;

namespace DeutschDeck.WebAPI.Utilities
{
    public class PasswordGenerationUtility
    {
        private Password _password = new Password(includeLowercase: true, includeUppercase: true, includeNumeric: false, includeSpecial: false, passwordLength: 16);
        public string Generate() 
        {
            return _password.Next();
        }
    }
}
