using Grpc.Core;
using PathDrift.Protos;

namespace PathDrift.Services
{
    public class CoordinatesService : CoordinatesPoster.CoordinatesPosterBase
    {
        private readonly ILogger<CoordinatesService> _logger;

        public CoordinatesService(
            ILogger<CoordinatesService> logger)
        {
            _logger = logger;

        }
        
        public override Task<CoordinatesReply> GetCoordinates(CoordinatesRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Request: {request.CoordinatesList}");
            return Task.FromResult(new CoordinatesReply
            {
                Message = "Coordinates received"
            });
        }
    }
}
