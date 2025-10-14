using PacketHandlers;
using Token;

namespace Routers;

public static class TokenRouters {

    public static RouteGroupBuilder TokenRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/token").AllowAnonymous();

        // POST /v1.0/token
        app.MapPost("", (HttpRequest request) =>

            PacketUtils.validate_and_reply(request, "token/get", async (packet) => {

                CreatedToken created_token = Token.TokenHandler.create_access_token((string) packet.body!["username"],"admin");

                var response = new Dictionary<string, object> {
                    ["access_token"] = created_token.access_token,
                    ["expires_in"] = created_token.expires_in,
                    ["type"] = created_token.token_type
                };

                return await Task.FromResult(PacketUtils.send_packet(new PacketSuccess(200,response)));

            })

        );

/*
        auth.MapGet("/secret", (HttpRequest request) =>

            PacketUtils.validate_and_reply(request, "token/get2", async (packet) => {
                return Results.Ok(new { message = "Você acessou um endpoint protegido!" });
            })

        );*/


        return group;

    }
}