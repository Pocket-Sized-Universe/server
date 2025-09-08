namespace PocketSizedUniverseSeeder;

public class Env
{
    public static string FilePath = Environment.GetEnvironmentVariable("FILE_PATH") ?? throw new ArgumentNullException($"FILE_PATH not set");
    public static string BaseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? throw new ArgumentNullException($"BASE_URL not set");
    public static int PollingInterval = int.Parse(Environment.GetEnvironmentVariable("POLLING_INTERVAL") ?? "60");
    public static int BatchSize = int.Parse(Environment.GetEnvironmentVariable("BATCH_SIZE") ?? "100");
}