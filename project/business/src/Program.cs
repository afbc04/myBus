var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://*:80");

ProgramHandler.load_templates();
//Controller.ControllerManager.

var app = builder.Build();

app.Use(ProgramHandler.log_requests);

v1Routers.Register(app);

app.Run();
