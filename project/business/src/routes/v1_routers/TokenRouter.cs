using PacketHandlers;
using Token;

namespace Routers;

public static class TokenRouters {

    public static RouteGroupBuilder TokenRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/token").AllowAnonymous();

        // POST /v1.0/token
        app.MapPost("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "token/create", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.create_token(new TokenRequestWrapper( packet.body!)));
            })

        );

        return group;

    }
}