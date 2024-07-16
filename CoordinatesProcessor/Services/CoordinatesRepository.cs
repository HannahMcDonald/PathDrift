using CoordinatesProcessor.Protos;

namespace CoordinatesProcessor.Services
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        private ILogger<CoordinatesRepository> _logger;

        private IEnumerable<Coordinates> _repository;
        public CoordinatesRepository(ILogger<CoordinatesRepository> logger)
        {
            _logger = logger;
        }

        public void StoreCoordinates(IEnumerable<Coordinates> coordinates)
        {
            _repository = coordinates;
        }

        public IEnumerable<Coordinates> GetCoordinates()
        {
            return _repository;
        }
    }
}
