namespace PacketHandlers {

    public class PacketUtils {

        public static async Task<IResult> validate_and_reply(HttpRequest request, string templateID, Func<PacketExtracted, Task<IResult>> act_if_valid) {

            (bool is_packet_valid, PacketFail? packet_failed, PacketExtracted? packet_extracted) = await PacketValidator.validate_packet(request, templateID);
            if (is_packet_valid is false)
                return send_packet(packet_failed!);

            return await act_if_valid(packet_extracted!);

        }

        public static IResult send_packet(SendingPacket packet) {

            if (packet is PacketSuccess packet_success) {

                if (packet_success.body is null)
                    return Results.StatusCode(packet_success.status_code);
                else
                    return Results.Json(packet_success.body, statusCode: packet_success.status_code);
            }

            else 
                return _send_error((PacketFail) packet);

        }

        private static IResult _send_error(PacketFail packet) {
            
            var json = new Dictionary<string, object>();

            if (packet.error_message != null)
                json["error"] = packet.error_message;

            if (packet.extra_message is not null) {
                foreach (var entry in packet.extra_message)
                    json[entry.Key] = entry.Value;
            }

            if (packet.error_message == null)
                return Results.StatusCode(packet.status_code);
            else
                return Results.Json(json,statusCode: packet.status_code);

        }

        public static object? get_value(IDictionary<string,object> dict, string key) {

            if (dict.ContainsKey(key))
                return dict[key];
            else
                return null;

        }

    }

}