using PacketHandlers;
using Token;
using Models;
using Nito.AsyncEx;
using Pages;

namespace Controller {

    public class CountryCodeController {

        public readonly AsyncReaderWriterLock Lock;
        private CountryCodeModel _model;
        private long _count;

        public CountryCodeController() {
            this.Lock = new();
            this._model = new CountryCodeModel();
            this._count = this._model.size().Result;
        }

        // ------------------ public methods ------------------

        public async Task<SendingPacket> create(AccessToken? token, string id, string name) {

            id = id.ToUpper();

            CountryCode? valid_country_code = validate_country_code(id, name);
            if (valid_country_code == null)
                return new PacketFail(417);

            bool inserted_country_code = await this._model.insert(valid_country_code!);
            if (inserted_country_code == false)
                return new PacketFail(422);

            this._count++;
            return new PacketSuccess(201, valid_country_code!.to_json());

        }

        public async Task<SendingPacket> list(AccessToken? token, PageInput page_input) {

            var list = await this._model.values(page_input);
            var country_code_list = list.Select(cc => (object) cc.to_json()).ToList();
            var page_output = new PageOutput(page_input,this._count,country_code_list);
            
            return new PacketSuccess(200, page_output.to_json());

        }

        public async Task<SendingPacket> clear(AccessToken? token) {

            var list = await this._model.clear();
            var country_code_list = list.Select(cc => (object) cc.to_json()).ToList();
            this._count = 0;

            return new PacketSuccess(200, country_code_list);

        }

        public async Task<SendingPacket> get(AccessToken? token, string id) {

            CountryCode? country_code = await this._model.get(id.ToUpper());
            if (country_code == null)
                return new PacketFail(404);

            return new PacketSuccess(200, country_code.to_json());

        }

        public async Task<SendingPacket> delete(AccessToken? token, string id) {

            id = id.ToUpper();

            CountryCode? country_code = await this._model.get(id);
            if (country_code == null)
                return new PacketFail(404);

            bool was_deleted = await this._model.delete(id);
            if (was_deleted == false)
                return new PacketFail(422);

            this._count--;
            return new PacketSuccess(200, country_code.to_json());

        }

        public async Task<SendingPacket> update(AccessToken? token, string id, string? name) {

            id = id.ToUpper();

            CountryCode? country_code = await this._model.get(id);
            if (country_code == null)
                return new PacketFail(404);

            CountryCode? valid_country_code = validate_country_code(
                id, 
                name ?? country_code.name
                );
            if (valid_country_code == null)
                return new PacketFail(417);

            bool updated_country_code = await this._model.update(valid_country_code!);
            if (updated_country_code == false)
                return new PacketFail(404);

            return new PacketSuccess(200, valid_country_code!.to_json());

        }

        // ------------------ private helper ------------------

        private CountryCode? validate_country_code(string id, string? name) {

            if (id.All(char.IsLetter) == false || 
                id.Length != CountryCode.idLength ||
                name is null ||
                name.Length > CountryCode.nameMaxLength
                )
                return null;


            return new CountryCode(id.ToUpper(), name);

        }
    }
}
