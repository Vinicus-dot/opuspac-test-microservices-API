using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Util
    {
        public static readonly string QueueProduct = GetEnvironmentVariable("PRODUCT_QUEUE");
        public static readonly string RabbitConnection = GetEnvironmentVariable("RABBIT_CONNECTION");
        public static readonly JwtToken _jwtToken = new(GetEnvironmentVariable("ENCRYPTION_CLAIMS_KEY"));
        public static string GetEnvironmentVariable(string variable)
        {
            string? env = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(env))
                throw new Exception($"environment variable {variable} not found!");
            return env;
        }

        public static bool IsValidEmail(string email)
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
    }
}
