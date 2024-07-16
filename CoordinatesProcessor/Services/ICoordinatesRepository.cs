using CoordinatesProcessor.Protos;

namespace CoordinatesProcessor.Services
{
    public interface ICoordinatesRepository
    {
        void StoreCoordinates(IEnumerable<Coordinates> coordinates);
        IEnumerable<Coordinates> GetCoordinates();
    }
}