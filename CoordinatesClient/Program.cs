using CoordinatesClient.Protos;
using Grpc.Net.Client;
using CoordinatesClient;

using var channel = GrpcChannel.ForAddress("https://localhost:7126");
var client = new CoordinatesApi.CoordinatesApiClient(channel);
var csvReader = new CsvReader();
var coordinatesList = csvReader.ReadData("CoordinatesClient.run1.csv");
var request = new CoordinatesRequest();
request.CoordinatesList.Add(coordinatesList);
var reply = client.GetCoordinates(request);

Console.WriteLine($"Reply : {reply.Message}");

Console.WriteLine($"Press any key to exit");
Console.ReadLine();