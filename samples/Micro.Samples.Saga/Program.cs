using Micro.Framework;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo);

app.MapGet("/ping", () => "pong");
    
app.UseMicroFramework().Run();