using PacketHandlers;
using Token;
using Models;
using Nito.AsyncEx;
using Pages;
using Queries;
using System.Text.RegularExpressions;

namespace Controller {

    public class UserController {

        public readonly AsyncReaderWriterLock Lock;
        private UserModel _model;
        private long _count;

        public UserController() {
            this.Lock = new();
            this._model = new UserModel();
            this._count = this._model.size().Result;
        }

        // ------------------ public methods ------------------

        public async Task<SendingPacket> create(AccessToken? token, bool country_code_exists, UserRequestWrapper request_wrapper) {

            var error_lists = validate_user(request_wrapper, country_code_exists);
            if (error_lists.Count() > 0)
                return new PacketFail("User is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            User user_to_be_inserted = request_wrapper.convert(this._count == 0);

            if (await this._model.contains(user_to_be_inserted.ID))
                return new PacketFail("ID is already registered",422);

            bool inserted_user = await this._model.insert(user_to_be_inserted);
            if (inserted_user == false)
                return new PacketFail("Failed when inserting user into the database",422);

            this._count++;
            var inserted_full_user = await this._model.get_full(user_to_be_inserted.ID);

            return new PacketSuccess(201, inserted_full_user!.to_json());

        }

        public async Task<SendingPacket> list(AccessToken? token, QueryUser query) {

            return new PacketSuccess(200);
            /*
            var listing_errors = page_request.validate(new Dictionary<string,string>{
                {"id", "id"},
                {"name", "name"},
                {"account_creation", "accountCreation"}
                });
            if (listing_errors.Count() > 0)
                return new PacketFail("Listing parameters are not valid",417,new Dictionary<string,object>(){ ["errors"] = listing_errors});

            var page_input = page_request.convert();
            var list = await this._model.list(page_input);
            var user_list = list.Select(u => (object) u.to_json()).ToList();
            var page_output = new PageOutput(page_input,this._count,user_list);
            
            return new PacketSuccess(200, page_output.to_json());*/

        }

        public async Task<SendingPacket> clear(AccessToken? token) {

            var list = await this._model.clear();
            var user_list = list.Select(u => (object) u.to_json()).ToList();
            this._count = 0;

            return new PacketSuccess(200, user_list);

        }

        public async Task<bool> contains(AccessToken? token, string id) {
            return await this._model.contains(id);
        }

        public async Task<SendingPacket> get(AccessToken? token, string id) {

            UserFull? user = await this._model.get_full(id);
            if (user == null)
                return new PacketFail(404);

            return new PacketSuccess(200, user.to_json());

        }

        public async Task<SendingPacket> delete(AccessToken? token, string id) {

            UserFull? user = await this._model.get_full(id);
            if (user == null)
                return new PacketFail(404);

            bool was_deleted = await this._model.delete(id);
            if (was_deleted == false)
                return new PacketFail("Failed when deleting user in database",422);

            this._count--;
            return new PacketSuccess(200, user.to_json());

        }

        public async Task<SendingPacket> update(AccessToken? token, string id, UserRequestWrapper request_wrapper, bool country_code_exists) {

            User? user = await this._model.get(id);
            if (user == null)
                return new PacketFail(404);

            request_wrapper.auto_fill(user);

            var error_lists = validate_user(request_wrapper, country_code_exists);
            if (error_lists.Count() > 0)
                return new PacketFail("User is not valid",417,new Dictionary<string,object>(){ ["errors"] = error_lists});

            var updated_user = request_wrapper.convert(false);

            Console.WriteLine($"WHAT PASSWORD {updated_user.password}");

            bool was_user_updated = await this._model.update(updated_user);
            if (was_user_updated == false)
                return new PacketFail("Failed when updating user into the database",422);

            var updated_full_user = await this._model.get_full(id);
            return new PacketSuccess(200, updated_full_user!.to_json());

        }

        // ------------------ private helper ------------------

        private IList<string> validate_user(UserRequestWrapper request, bool country_code_exists) {

            var error_list = new List<string>();

            if (Regex.IsMatch(request.id,@"^\w[\w\d_.-]*$") == false)
                error_list.Add("ID is in a incorrect format");
            if (request.id.Length > User.idMaxLength)
                error_list.Add($"ID is too bigger. Limit is {User.idMaxLength} characters");

            if (request.password is null)
                error_list.Add($"Password must exists");
            else if (request.new_password == true && (request.password.Length < User.passwordMinLength || request.password.Length > User.passwordMaxLength))
                error_list.Add($"Password's length is out of bounds [{User.passwordMinLength} - {User.passwordMaxLength}] {request.password}");

            if (request.name != null) {

                if (Regex.IsMatch(request.name,@"^\w[\w\s]*$") == false)
                    error_list.Add("Name is in a incorrect format");
                if (request.id.Length > User.nameMaxLength)
                    error_list.Add($"Name is too bigger. Limit is {User.nameMaxLength} characters");
            
            }

            if (request.email != null) {

                if (Regex.IsMatch(request.email,@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$") == false)
                    error_list.Add("Email is in a incorrect format");
                if (request.id.Length > User.emailMaxLength)
                    error_list.Add($"Email is too bigger. Limit is {User.emailMaxLength} characters");
            
            }

            if (request.birth_date != null) {

                if (DateOnly.TryParse(request.birth_date, out DateOnly valid_birth_date)) {
                    if (valid_birth_date >= DateOnly.FromDateTime(DateTime.Now))
                        error_list.Add($"Birth Date must be before the current date...");
                }
                else
                    error_list.Add($"Invalid Birth Date (yyyy-MM-dd)");

            }

            if (request.sex != null) {

                string upper_sex = request.sex.ToUpper();
                List<string> possible_sex = new(){"F","FEMALE","M","MALE","O","NONSPEFICIED"};

                if (possible_sex.Contains(upper_sex) == false)
                    error_list.Add($"Sex is incorrect. Try these : [{string.Join(", ",possible_sex)}]");
                else {
                    request.sex = upper_sex switch {
                        "FEMALE" => "F",
                        "F" => "F",
                        "MALE" => "M",
                        "M" => "M",
                        _ => "O"
                    };
                }       

            }

            if (request.country_code_id != null && country_code_exists == false)
                error_list.Add("Country Code does not exists");

            Console.WriteLine($"NEW PASSWORD {request.password}");

            return error_list;

        }
    }
}
