using Serilog;

public class Program {

    public static void Main(string[] args) {

        if (API.init() == true) {

            Log.Information("API was successfully started");

            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("http://*:80");

            var app = builder.Build();
            app.Use(ProgramHandler.log_requests);
            v1Routers.Register(app);

            app.Run();

        }
        else {
            Log.Error("API couldn't start");
        }

    }

}
