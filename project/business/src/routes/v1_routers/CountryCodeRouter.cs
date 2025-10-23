using PacketHandlers;
using Queries;

namespace Routers;

public static class CountryCodeRouters {

    public static RouteGroupBuilder CountryCodeRoutersMapping(this RouteGroupBuilder group) {

        var app = group.MapGroup("/countryCodes").AllowAnonymous();

        // POST /v1.0/countryCodes
        app.MapPost("", async (HttpRequest request) => {

            return await PacketUtils.validate_and_reply(request, "countryCodes/create", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.create_country_code(

                    packet.access_token,
                    new CountryCodeRequestWrapper((string) packet.body!["id"] ,packet.body!)
                    
                    ));
            });

        });


        // GET /v1.0/countryCodes
        app.MapGet("", async (HttpRequest request) => {

            return await PacketUtils.validate_and_reply(request, "countryCodes/list", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.list_country_code(

                    packet.access_token,
                    new QueryCountryCode(packet.queries!)

                    ));
            });

        });

        // DELETE /v1.0/countryCodes
        app.MapDelete("", async (HttpRequest request) =>

            await PacketUtils.validate_and_reply(request, "countryCodes/delete", async (packet) => {
                return PacketUtils.send_packet(await API.controller!.clear_country_code(packet.access_token));
            })

        );

        // GET /v1.0/countryCodes/{id}
        app.MapGet("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "countryCodes/get", async (packet) => {

                var result = await API.controller!.get_country_code(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // DELETE /v1.0/countryCodes/{id}
        app.MapDelete("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "countryCodes/delete", async (packet) => {

                var result = await API.controller!.delete_country_code(packet.access_token, id);
                return PacketUtils.send_packet(result);

            })

        );

        // PUT /v1.0/countryCodes/{id}
        app.MapPut("{id}", async (HttpRequest request, string id) =>

            await PacketUtils.validate_and_reply(request, "countryCodes/update", async (packet) => {

                return PacketUtils.send_packet(await API.controller!.update_country_code(

                    packet.access_token,
                    id,
                    new CountryCodeRequestWrapper(id,packet.body!)

                ));

            })

        );

        return group;

    }

}