
using static System.Net.WebRequestMethods;

namespace CoordinatesClient
{
    public class Settings
    {
        public string FilePath { get; set; } = string.Empty;
        public int MaximumRetryAttempts { get; set; } = 5;
        public string CoordinatesProcesser { get; set; } = "https://localhost:7126";
    }
}
