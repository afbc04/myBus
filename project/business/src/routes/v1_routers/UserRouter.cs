using PacketHandlers;
using Token;
using Pages;
using Queries;

namespace Routers;

public static class UserRouters {

    public static RouteGroupBuilder UserRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/users").AllowAnonymous();

        // POST /v1.0/users
        app.MapPost("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "users/create", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.create_user(

                    packet.access_token,
                    new UserRequestWrapper((string) packet.body!["id"] ,packet.body!)
                    
                    ));
            })

        );


        // GET /v1.0/users
        app.MapGet("", async (HttpRequest request) => {

            return await PacketUtils.validate_and_reply(request, "users/get", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.list_user(

                    packet.access_token,
                    new QueryUser(packet.queries!)

                    ));
            });

        });

        // DELETE /v1.0/users
        app.MapDelete("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "users/delete", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.clear_user(packet.access_token));
            })

        );

        // GET /v1.0/users/{id}
        app.MapGet("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "users/get", async (packet) => {

                var result = await API.controller!.get_user(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // DELETE /v1.0/users/{id}
        app.MapDelete("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "users/delete", async (packet) => {

                var result = await API.controller!.delete_user(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // PUT /v1.0/users/{id}
        app.MapPut("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "users/update", async (packet) => {

                return PacketUtils.send_packet(await API.controller!.update_user(

                    packet.access_token,
                    id,
                    new UserRequestWrapper(id,packet.body!)

                ));

            })

        );

        return group;

    }

}