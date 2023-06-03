using ApiGateway;

internal class Program
{
	private static void Main(string[] args)
	{
		CreateHostBuilder(args).Build().Run();
	}

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
		.ConfigureAppConfiguration((hosting, config) =>
		{
			config.AddJsonFile("ocelot.json", false, true);
		})
		.ConfigureWebHostDefaults(webBuilder =>
		{
			webBuilder.UseStartup<Startup>();
		});
}