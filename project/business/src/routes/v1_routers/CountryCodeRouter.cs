using PacketHandlers;
using Token;

namespace Routers;

public static class CountryCodeRouters {

    public static RouteGroupBuilder CountryCodeRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/countryCodes").AllowAnonymous();

        // POST /v1.0/countryCodes
        app.MapPost("", (HttpRequest request) =>

            PacketUtils.validate_and_reply(request, "countryCodes/create", async (packet) => {
                return await Task.FromResult(PacketUtils.send_packet(Controller.ControllerManager.controller.createCountryCode(
                    packet.access_token,
                    (string) packet.body!["id"],
                    (string) packet.body!["name"]
                    )));
            })

        );

        // GET /v1.0/countryCodes
        app.MapGet("", (HttpRequest request) =>

            PacketUtils.validate_and_reply(request, "countryCodes/get", async (packet) => {
                return await Task.FromResult(PacketUtils.send_packet(Controller.ControllerManager.controller.listCountryCode(
                    packet.access_token
                    )));
            })

        );

        // GET /v1.0/countryCodes/{id}
        app.MapGet("{id}", (HttpRequest request, string id) =>

            PacketUtils.validate_and_reply(request, "countryCodes/get", async (packet) => {

                var result = Controller.ControllerManager.controller.getCountryCode(packet.access_token, id);
                return await Task.FromResult(PacketUtils.send_packet(result));

            })

        );

        // DELETE /v1.0/countryCodes/{id}
        app.MapDelete("{id}", (HttpRequest request, string id) =>

            PacketUtils.validate_and_reply(request, "countryCodes/delete", async (packet) => {

                var result = Controller.ControllerManager.controller.deleteCountryCode(packet.access_token, id);
                return await Task.FromResult(PacketUtils.send_packet(result));

            })

        );

        // PUT /v1.0/countryCodes/{id}
        app.MapPut("{id}", (HttpRequest request, string id) =>

            PacketUtils.validate_and_reply(request, "countryCodes/update", async (packet) => {

                var result = Controller.ControllerManager.controller.updateCountryCode(
                    packet.access_token,
                    id,
                    (string) packet.body!["name"]
                    );
                return await Task.FromResult(PacketUtils.send_packet(result));

            })

        );

        // PATCH /v1.0/countryCodes/{id}
        app.MapPatch("{id}", (HttpRequest request, string id) =>

            PacketUtils.validate_and_reply(request, "countryCodes/patch", async (packet) => {

                var result = Controller.ControllerManager.controller.patchCountryCode(
                    packet.access_token,
                    id,
                    (string?) PacketUtils.get_value(packet.body!,"name")
                    );
                return await Task.FromResult(PacketUtils.send_packet(result));
                
            })

        );

        return group;

    }
}