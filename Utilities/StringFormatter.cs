using System.Text.RegularExpressions;

namespace DeutschDeck.WebAPI.Utilities
{
    public class StringFormatter
    {
        public static string Format(string template, params string[] values)
        {
            return Regex.Replace(template, @"{(\d+)}", match => {
                int index = int.Parse(match.Groups[1].Value);
                return index < values.Length ? values[index] : match.Value;
            });
        }
    }
}
