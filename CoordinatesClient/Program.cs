using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CoordinatesClient;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var poster = serviceProvider.GetService<IGrpcClient>();

        if (poster is not null)
        {
            var result = await poster.PostAsync();
            Console.WriteLine(result.Message);
        }

        
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        var configuration = GetConfigurationSources(Environment.CurrentDirectory, "Development");
        services.AddSingleton<IConfiguration>(configuration);

        services.Configure<Settings>(configuration.GetSection("Settings"));

        services.AddLogging(configure => configure
        .AddConfiguration(configuration.GetSection("Logging"))
        .AddConsole()
        .AddDebug());

        services.AddTransient<IGrpcClient, GrpcClient>();
        services.AddTransient<ICsvReader, CsvReader>();
    }

    private static IConfigurationRoot GetConfigurationSources(string basePath, string environment)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile(
                path: "appsettings.json",
                optional: true,
                reloadOnChange: true);

        return configurationBuilder.Build();
    }
}

/*
var grpcClient = new GrpcClient()
using var channel = GrpcChannel.ForAddress("https://localhost:7126");
var client = new CoordinatesApi.CoordinatesApiClient(channel);
var csvReader = new CsvReader();
var coordinatesList = csvReader.ReadData("CoordinatesClient.run1.csv");

var request = new CoordinatesRequest();
request.CoordinatesList.Add(coordinatesList);
var reply = client.GetCoordinates(request);

Console.WriteLine($"Reply : {reply.Message}");

Console.WriteLine($"Press any key to exit");
Console.ReadLine();
*/