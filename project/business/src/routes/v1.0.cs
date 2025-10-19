using Routers;

public static class v1Routers {

    public static void Register(WebApplication app) {
        
        var api = app.MapGroup("/v1.0");

        api.TokenRoutersMapping();
        api.CountryCodeRoutersMapping();
        api.BusPassRoutersMapping();

    }

}
