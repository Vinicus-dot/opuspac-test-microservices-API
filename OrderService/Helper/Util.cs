namespace OrderService.Helper
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
