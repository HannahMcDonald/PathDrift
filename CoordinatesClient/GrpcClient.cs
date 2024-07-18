using CoordinatesClient.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace CoordinatesClient
{
    public class GrpcClient : IGrpcClient
    {
        private readonly IOptions<Settings> _settings;
        private readonly ICsvReader _csvReader;
        private readonly ILogger<GrpcClient> _logger;

        public GrpcClient(
            IOptions<Settings> settings,
            ICsvReader csvReader,
            ILogger<GrpcClient> logger )
        {
            _settings = settings;
            _csvReader = csvReader;
            _logger = logger;
        }

        public async Task<CoordinatesReply> PostAsync()
        {
            var coordinatesList = _csvReader.ReadData("CoordinatesClient.run1.csv");
            var attempts = 0;
            do
            {

                try
                {
                    attempts++;

                    _logger.LogInformation("PostAync - Trying to post to CoordinatesProcessor, attempt: {attempts} of {maxAttempts}",
                        attempts,
                        _settings.Value.MaximumRetryAttempts);

                    // for http local host
                    //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    using var channel = GrpcChannel.ForAddress(_settings.Value.CoordinatesProcesser);
                    var client = new CoordinatesApi.CoordinatesApiClient(channel);
                   

                    var request = new CoordinatesRequest();
                    request.CoordinatesList.Add(coordinatesList);

                    var reply = client.GetCoordinates(request);
                    return client.GetCoordinates(request);

                }
                catch (Exception e)
                {
                    if (attempts == _settings.Value.MaximumRetryAttempts)
                    {
                        throw new InvalidOperationException($"Failed to post coordinates  - {attempts} made of {_settings.Value.MaximumRetryAttempts}, Exception {e}");
                    }

                    //Back off retry
                    if (attempts == 1)
                    {
                        _logger.LogError("Failed to post coordinates - { attempts} made of { _settings.Value.MaximumRetryAttempts} will retry with a 30 second back off, Exception {e}",
                            attempts,
                            _settings.Value.MaximumRetryAttempts,
                            e);
                        await Task.Delay(TimeSpan.FromSeconds(30));
                    }
                    else if (attempts == 3)
                    {
                        _logger.LogError("Failed to post coordinates - { attempts} made of { _settings.Value.MaximumRetryAttempts} will retry with a 5 minutes back off, Exception {e}",
                            attempts,
                            _settings.Value.MaximumRetryAttempts,
                            e);
                        await Task.Delay(TimeSpan.FromMinutes(5));
                    }
                }
            } while (true);
        }
    }
}
