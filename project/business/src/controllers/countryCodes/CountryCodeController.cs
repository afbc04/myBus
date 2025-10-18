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

        public async Task<SendingPacket> create(AccessToken? token, CountryCodeRequestWrapper request_wrapper) {

            var error_lists = validate_country_code(request_wrapper);
            if (error_lists.Count() > 0)
                return new PacketFail("Country code is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            CountryCode country_code_to_be_inserted = request_wrapper.convert();

            if (await this._model.contains(country_code_to_be_inserted.ID))
                return new PacketFail("ID is already registered",422);

            bool inserted_country_code = await this._model.insert(country_code_to_be_inserted);
            if (inserted_country_code == false)
                return new PacketFail("Failed when inserting country code into the database",422);

            this._count++;
            return new PacketSuccess(201, country_code_to_be_inserted.to_json());

        }

        public async Task<SendingPacket> list(AccessToken? token, PageRequest page_request) {

            var listing_errors = page_request.validate(new Dictionary<string,string>{
                {"id", "id"},
                {"name", "name"}
                });
            if (listing_errors.Count() > 0)
                return new PacketFail("Listing parameters are not valid",417,new Dictionary<string,object>(){ ["errors"] = listing_errors});

            var page_input = page_request.convert();
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

        public async Task<bool> contains(AccessToken? token, string id) {
            return await this._model.contains(id);
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
                return new PacketFail("Failed when deleting country code in database",422);

            this._count--;
            return new PacketSuccess(200, country_code.to_json());

        }

        public async Task<SendingPacket> update(AccessToken? token, string id, CountryCodeRequestWrapper request_wrapper) {

            id = id.ToUpper();

            CountryCode? country_code = await this._model.get(id);
            if (country_code == null)
                return new PacketFail(404);

            request_wrapper.auto_fill(country_code);

            var error_lists = validate_country_code(request_wrapper);
            if (error_lists.Count() > 0)
                return new PacketFail("Country code is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            var updated_country_code = request_wrapper.convert();

            bool was_country_code_updated = await this._model.update(updated_country_code);
            if (was_country_code_updated == false)
                return new PacketFail("Failed when updating country code into the database",422);

            return new PacketSuccess(200, updated_country_code.to_json());

        }

        // ------------------ private helper ------------------

        private IList<string> validate_country_code(CountryCodeRequestWrapper request) {

            var error_list = new List<string>();

            if (request.id.All(char.IsLetter) == false)
                error_list.Add("ID must only contains letters");
            if (request.id.Length != CountryCode.idLength)
                error_list.Add($"ID must be {CountryCode.idLength} letters");

            if (request.name is null)
                error_list.Add($"Name must exists");
            else if (request.name.Length > CountryCode.nameMaxLength)
                error_list.Add($"Name is too big. Cant be bigger than {CountryCode.nameMaxLength} characters");

            return error_list;

        }
    }
}
