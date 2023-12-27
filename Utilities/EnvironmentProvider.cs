using dotenv.net;
using System.Runtime.InteropServices;

namespace DeutschDeck.WebAPI.Utilities
{
    public class EnvironmentFallbackProvider
    {
        public static IDictionary<string, string> _dotEnv = DotEnv.Read();
        public static string GetEnvironmentVariable(string key) 
        {
            var envVariable = Environment.GetEnvironmentVariable(key);
            if (envVariable is not null) 
                return envVariable;

            var fallbackVariable = _dotEnv[key];
            if (fallbackVariable is not null) 
                return fallbackVariable;

            throw new Exception(string.Format("Unable to find key:{0} in env variables", key));
        }
    }
}
