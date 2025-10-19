using PacketHandlers;
using Token;
using Pages;

namespace Routers;

public static class BusPassRouters {

    public static RouteGroupBuilder BusPassRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/BusPasses").AllowAnonymous();

        // POST /v1.0/BusPasses
        app.MapPost("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "busPasses/create", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.create_bus_pass(

                    packet.access_token,
                    new BusPassRequestWrapper((string) packet.body!["id"], packet.body!)
                    
                    ));
            })

        );

        // GET /v1.0/BusPasses
        app.MapGet("", async (HttpRequest request) => {

            return await PacketUtils.validate_and_reply(request, "busPasses/get", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.list_bus_pass(

                    packet.access_token,
                    new PageRequest(request)

                    ));
            });

        });

        // DELETE /v1.0/BusPasses
        app.MapDelete("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "busPasses/delete", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.clear_bus_pass(packet.access_token));
            })

        );

        // GET /v1.0/BusPasses/{id}
        app.MapGet("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "busPasses/get", async (packet) => {

                var result = await API.controller!.get_bus_pass(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // DELETE /v1.0/BusPasses/{id}
        app.MapDelete("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "busPasses/delete", async (packet) => {

                var result = await API.controller!.delete_bus_pass(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // PUT /v1.0/BusPasses/{id}
        app.MapPut("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "busPasses/update", async (packet) => {

                return PacketUtils.send_packet(await API.controller!.update_bus_pass(

                    packet.access_token,
                    id,
                    new BusPassRequestWrapper(id, packet.body!)

                ));

            })

        );

        return group;

    }

}