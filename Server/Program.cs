var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "server hsa started!");

app.Run();