using CoordinatesClient.Protos;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;

namespace CoordinatesClient
{
    public class CsvReader : ICsvReader
    {
        private readonly ILogger<CsvReader> _logger;

        public CsvReader(ILogger<CsvReader> logger) 
        { 
            _logger = logger;
        }
        public List<Coordinates> ReadData(string filePath)
        {
            _logger.LogInformation("Reading csv data from {filepath}", filePath);

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("ReadData - No filepath for data specified");
            }

            List<Coordinates> coordinatesList = new();

            using (TextFieldParser parser = new(ResourceHelper.GetResourceAsStream(filePath)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                if (!parser.EndOfData)
                {
                    parser.ReadLine(); // Skip the header line
                }

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();

                    if (fields is null)
                    {
                        return coordinatesList;
                    }
                    else
                    {
                        var coordinates = new Coordinates
                        {
                            Id = fields[0] ?? "",
                            Index = int.Parse(fields[1]),
                            XCoordinate = float.Parse(fields[2]),
                            YCoordinate = float.Parse(fields[3]),
                            ZCoordinate = float.Parse(fields[4]),
                            RxCoordinate = float.Parse(fields[5]),
                            RyCoordinate = float.Parse(fields[6]),
                            RzCoordinate = float.Parse(fields[7]),
                            UxCoordinate = float.Parse(fields[8]),
                            UyCoordinate = float.Parse(fields[9]),
                            UzCoordinate = float.Parse(fields[10]),
                            UTranslation = float.Parse(fields[11]),
                            URxCoordinate = float.Parse(fields[12]),
                            URyCoordinate = float.Parse(fields[13]),
                            URzCoordinate = float.Parse(fields[14])
                        };

                        coordinatesList.Add(coordinates);
                    }
                }
            }

            return coordinatesList;
        }
    }
}
