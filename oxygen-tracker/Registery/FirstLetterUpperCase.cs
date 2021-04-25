using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oxygen_tracker.Registery
{
    public static class FirstLetterUpperCase
    {
        public static string ToFirstUpperCase(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };
    }
}
