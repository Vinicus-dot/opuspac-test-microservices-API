using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Helper
{
    public class Util
    {
        public static string GetEnvironmentVariable(string variable)
        {
            string? env = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(env))
                throw new Exception($"environment variable {variable} not found!");
            return env;
        }
    }
}
