using CoordinatesProcessor.Components;
using CoordinatesProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddGrpc();

builder.Services
    .AddSingleton<ICoordinatesRepository, CoordinatesRepository>();
    


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure RPC service
app.MapGrpcService<CoordinatesRpcService>();
app.MapGet("/protos/coordinates.proto", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("Protos/coordinates.proto"));
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
