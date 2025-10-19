using PacketHandlers;
using Token;
using Models;
using Nito.AsyncEx;
using Pages;

namespace Controller {

    public class BusPassController {

        public readonly AsyncReaderWriterLock Lock;
        private BusPassModel _model;
        private long _count;

        public BusPassController() {
            this.Lock = new();
            this._model = new BusPassModel();
            this._count = this._model.size().Result;
        }

        // ------------------ public methods ------------------

        public async Task<SendingPacket> create(AccessToken? token, BusPassRequestWrapper request_wrapper) {

            var error_lists = validate_bus_pass(request_wrapper);
            if (error_lists.Count() > 0)
                return new PacketFail("Bus pass is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            BusPass bus_pass_to_be_inserted = request_wrapper.convert();

            if (await this._model.contains(bus_pass_to_be_inserted.ID))
                return new PacketFail("ID is already registered",422);

            bool inserted_bus_pass = await this._model.insert(bus_pass_to_be_inserted);
            if (inserted_bus_pass == false)
                return new PacketFail("Failed when inserting bus pass into the database",422);

            this._count++;
            return new PacketSuccess(201, bus_pass_to_be_inserted.to_json());

        }

        public async Task<SendingPacket> list(AccessToken? token, PageRequest page_request) {

            var listing_errors = page_request.validate(new Dictionary<string,string>{
                {"id", "id"},
                {"discount", "discount"},
                {"locality_level", "localityLevel"},
                {"duration", "duration"},
                });
            if (listing_errors.Count() > 0)
                return new PacketFail("Listing parameters are not valid",417,new Dictionary<string,object>(){ ["errors"] = listing_errors});

            var page_input = page_request.convert();
            var list = await this._model.values(page_input);
            var bus_pass_list = list.Select(cc => (object) cc.to_json()).ToList();
            var page_output = new PageOutput(page_input,this._count,bus_pass_list);
            
            return new PacketSuccess(200, page_output.to_json());

        }

        public async Task<SendingPacket> clear(AccessToken? token) {

            var list = await this._model.clear();
            var bus_pass_list = list.Select(cc => (object) cc.to_json()).ToList();
            this._count = 0;

            return new PacketSuccess(200, bus_pass_list);

        }

        public async Task<bool> contains(AccessToken? token, string id) {
            return await this._model.contains(id);
        }

        public async Task<SendingPacket> get(AccessToken? token, string id) {

            BusPass? bus_pass = await this._model.get(id.ToUpper());
            if (bus_pass == null)
                return new PacketFail(404);

            return new PacketSuccess(200, bus_pass.to_json());

        }

        public async Task<SendingPacket> delete(AccessToken? token, string id) {

            id = id.ToUpper();

            BusPass? bus_pass = await this._model.get(id);
            if (bus_pass == null)
                return new PacketFail(404);

            bool was_deleted = await this._model.delete(id);
            if (was_deleted == false)
                return new PacketFail("Failed when deleting bus pass in database",422);

            this._count--;
            return new PacketSuccess(200, bus_pass.to_json());

        }

        public async Task<SendingPacket> update(AccessToken? token, string id, BusPassRequestWrapper request_wrapper) {

            id = id.ToUpper();

            BusPass? bus_pass = await this._model.get(id);
            if (bus_pass == null)
                return new PacketFail(404);

            request_wrapper.auto_fill(bus_pass);

            var error_lists = validate_bus_pass(request_wrapper);
            if (error_lists.Count() > 0)
                return new PacketFail("Bus pass is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            var updated_bus_pass = request_wrapper.convert();

            bool was_bus_pass_updated = await this._model.update(updated_bus_pass);
            if (was_bus_pass_updated == false)
                return new PacketFail("Failed when updating bus pass into the database",422);

            return new PacketSuccess(200, updated_bus_pass.to_json());

        }

        // ------------------ private helper ------------------

        private IList<string> validate_bus_pass(BusPassRequestWrapper request) {

            var error_list = new List<string>();

            if (request.id.All(char.IsLetter) == false)
                error_list.Add("ID must only contains letters");
            if (request.id.Length != BusPass.idLength)
                error_list.Add($"ID must be {BusPass.idLength} letters");

            if (request.discount is null)
                error_list.Add($"Discount must exists");
            else if (request.discount < 0 || request.discount > 100)
                error_list.Add($"Discount must be a valid percentage [0 - 100]");

            if (request.localityLevel is null)
                error_list.Add($"Locality Level must exists");
            else if (request.localityLevel < 1 || request.localityLevel > 3)
                error_list.Add($"Locality Level not valid. Must be these: [1, 2, 3]");

            if (request.duration is null)
                error_list.Add($"Duration must exists");
            else if (request.duration < 1)
                error_list.Add($"Duration must be a positive number");

            return error_list;

        }
    }
}
