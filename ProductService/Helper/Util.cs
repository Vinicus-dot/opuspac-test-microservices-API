namespace ProductService.Helper
{
    public class Util
    {
        public static readonly string QueueProduct = GetEnvironmentVariable("PRODUCT_QUEUE");
        public static readonly string RabbitConnection = GetEnvironmentVariable("RABBIT_CONNECTION");
        public static string GetEnvironmentVariable(string variable)
        {
            string? env = Environment.GetEnvironmentVariable(variable);
            if (string.IsNullOrEmpty(env))
                throw new Exception($"environment variable {variable} not found!");
            return env;
        }
    }
}
