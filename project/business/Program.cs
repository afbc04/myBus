var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:25000");

ProgramHandler.load_templates();

var app = builder.Build();

app.Use(ProgramHandler.log_requests);

v1Routers.Register(app);

app.Run();
