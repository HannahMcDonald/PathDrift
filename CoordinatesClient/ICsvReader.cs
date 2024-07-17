using CoordinatesClient.Protos;

namespace CoordinatesClient
{
    public interface ICsvReader
    {
        List<Coordinates> ReadData(string filePath);
    }
}