using PacketHandlers;
using Token;

namespace Controller {

    public interface ICountryCodeController {
        SendingPacket createCountryCode(AccessToken? token, string id, string name);
        SendingPacket listCountryCode(AccessToken? token);
        SendingPacket getCountryCode(AccessToken? token, string id);
        SendingPacket deleteCountryCode(AccessToken? token, string id);
        SendingPacket updateCountryCode(AccessToken? token, string id, string name);
        SendingPacket patchCountryCode(AccessToken? token, string id, string? name);
    }

    public class CountryCodeController : ICountryCodeController {

        private Dictionary<string,CountryCode> ccdict;

        public CountryCodeController() {
            this.ccdict = new();
        }

        public SendingPacket createCountryCode(AccessToken? token, string id, string name) {

            id = id.ToUpper();

            if (ccdict.ContainsKey(id))
                return new PacketFail("ID is duplicated",422,null);

            (bool is_cc_valid, CountryCode? cc, PacketFail? packet_in_case_of_fail) = _validate_country_code(id, name);
            if (is_cc_valid == false)
                return packet_in_case_of_fail!;
            
            ccdict[id] = cc!;

            return new PacketSuccess(201,cc!.to_json());

        }

        public SendingPacket listCountryCode(AccessToken? token) {

            List<IDictionary<string,object>> list_to_be_send = new();

            foreach (var cc in ccdict.Values) 
                list_to_be_send.Add(cc.to_json());
            

            return new PacketSuccess(200,list_to_be_send);

        }

        public SendingPacket getCountryCode(AccessToken? token, string id) {

            id = id.ToUpper();

            if (!ccdict.ContainsKey(id))
                return new PacketFail(404);
            else
                return new PacketSuccess(200,ccdict[id].to_json());

        }

        public SendingPacket deleteCountryCode(AccessToken? token, string id) {

            id = id.ToUpper();

            if (!ccdict.ContainsKey(id))
                return new PacketFail(404);
            else {

                var deleted_country_code = ccdict[id];
                ccdict.Remove(id);

                return new PacketSuccess(200,deleted_country_code.to_json());
            }

        }

        public SendingPacket updateCountryCode(AccessToken? token, string id, string name) {

            id = id.ToUpper();

            if (!ccdict.ContainsKey(id))
                return new PacketFail(404);

            (bool is_cc_valid, CountryCode? cc, PacketFail? packet_in_case_of_fail) = _validate_country_code(id, name);
            if (is_cc_valid == false)
                return packet_in_case_of_fail!;
            
            ccdict[id] = cc!;

            return new PacketSuccess(200,cc!.to_json());

        }

        public SendingPacket patchCountryCode(AccessToken? token, string id, string? name) {

            id = id.ToUpper();

            if (!ccdict.ContainsKey(id))
                return new PacketFail(404);

            var updating_cc = ccdict[id];

            (bool is_cc_valid, CountryCode? cc, PacketFail? packet_in_case_of_fail) = _validate_country_code(
                id,
                name ?? updating_cc.name
                );
            if (is_cc_valid == false)
                return packet_in_case_of_fail!;
            
            ccdict[id] = cc!;

            return new PacketSuccess(200,cc!.to_json());

        }   


        private (bool, CountryCode?, PacketFail?) _validate_country_code(
            string ID,
            string? name
        ) {

            if (ID.All(char.IsLetter) == false) return (false, null, new PacketFail("ID must contain only letters",417));
            if (ID.Length != CountryCode.idLength) return (false, null, new PacketFail($"ID must be composed by {CountryCode.idLength} letters",417));

            if (name == null) return (false, null, new PacketFail("Name field is not present",417));
            if (name.Length > CountryCode.nameMaxLength) return (false, null, new PacketFail($"Name is too bigger. Expected maximum {CountryCode.nameMaxLength} characters, received {name.Length}",417));

            return (true, new CountryCode(ID, name), null);

        }

    }

}