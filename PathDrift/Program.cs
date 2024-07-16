using PathDrift.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CoordinatesService>();
app.MapGet("/protos/coordinates.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/coordinates.proto"));
});    

app.Run();
