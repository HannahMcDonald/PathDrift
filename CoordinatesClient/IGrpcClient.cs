using CoordinatesClient.Protos;

namespace CoordinatesClient
{
    public interface IGrpcClient
    {
        Task<CoordinatesReply> PostAsync();
    }
}