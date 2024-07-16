using CoordinatesProcessor.Protos;
using Grpc.Core;

namespace CoordinatesProcessor.Services
{
    public class CoordinatesRpcService : CoordinatesApi.CoordinatesApiBase
    {
        private readonly ILogger<CoordinatesRpcService> _logger;
        private readonly ICoordinatesRepository _coordinatesRepository;

        public CoordinatesRpcService(
            ILogger<CoordinatesRpcService> logger,
            ICoordinatesRepository coordinatesRepository)
        {
            _logger = logger;
            _coordinatesRepository = coordinatesRepository;
        }
        
        public override Task<CoordinatesReply> GetCoordinates(CoordinatesRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Request: {request.CoordinatesList}");

            if (request is null || request.CoordinatesList.Count == 0 )
            {
                _logger.LogWarning("CoordinatesRequest is null or empty no coordinate to store");
            }

            _coordinatesRepository.StoreCoordinates(request.CoordinatesList);

            return Task.FromResult(new CoordinatesReply
            {
                Message = "Coordinates received"
            });
        }
    }
}
