using CoordinatesClient.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace CoordinatesClient
{
    public class GrpcClient : IGrpcClient
    {
        private readonly IOptions<Settings> _settings;
        private readonly ICsvReader _csvReader;

        public GrpcClient(
            IOptions<Settings> settings,
            ICsvReader csvReader)
        {
            _settings = settings;
            _csvReader = csvReader;
        }

        public async Task<CoordinatesReply> PostAsync()
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    using var channel = GrpcChannel.ForAddress(_settings.Value.CoordinatesProcesser);
                    var client = new CoordinatesApi.CoordinatesApiClient(channel);
                    var coordinatesList = _csvReader.ReadData("CoordinatesClient.run1.csv");

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
                        await Task.Delay(TimeSpan.FromSeconds(30));
                    }
                    else if (attempts == 2)
                    {
                        await Task.Delay(TimeSpan.FromMinutes(5));
                    }
                }
            } while (true);
        }
    }
}
