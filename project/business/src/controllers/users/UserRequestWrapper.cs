using PacketHandlers;

public class UserRequestWrapper {

    public string id {get;set;}
    public string? password { get; set; }
    public string? name { get; set; }
    public string? email { get; set; }
    public string? birth_date { get; set; }
    public string? sex { get; set; }
    public string? country_code_id { get; set; }
    public bool? is_public { get; set; }
    public bool? is_disable_person { get; set; }

    public User? user_template { get; set; }
    public bool new_password { get; set; }

    public UserRequestWrapper(string id, IDictionary<string,object> body) {

        this.id = id;
        this.password = (string?) PacketUtils.get_value(body,"password");
        this.name = (string?) PacketUtils.get_value(body,"name");
        this.email = (string?) PacketUtils.get_value(body,"email");
        this.birth_date = (string?) PacketUtils.get_value(body,"birthDate");
        this.sex = (string?) PacketUtils.get_value(body,"sex");
        this.country_code_id = (string?) PacketUtils.get_value(body,"countryCode");
        this.is_public = (bool?) PacketUtils.get_value(body,"public");
        this.is_disable_person = (bool?) PacketUtils.get_value(body,"disablePerson");
        
        this.user_template = null;
        this.new_password = true;

    }

    public void auto_fill(User user) {

        this.id = user.ID;

        this.new_password = this.password != null;
        this.password =  this.password ?? user.password;

        this.name =  this.name ?? user.name;
        this.email =  this.email ?? user.email;
        this.birth_date =  this.birth_date ?? user.birth_date?.ToString("yyyy-MM-dd");
        this.sex =  this.sex ?? $"{user.sex}";
        this.country_code_id =  this.country_code_id ?? user.country_code;
        this.is_public =  this.is_public ?? user.is_public;
        this.is_disable_person =  this.is_disable_person ?? user.is_disable_person;

        this.user_template = user;

    }

    public User convert(bool should_be_admin) {

        char sex = this.sex == null ? 'O' : this.sex switch {
            "F" => 'F',
            "M" => 'M',
            _ => 'O'
        };

        DateOnly? final_birth_date = this.birth_date != null ? DateOnly.Parse(this.birth_date) : null;

        if (this.user_template == null)
            return User.register_user(this.id,this.password!,this.name,should_be_admin,this.email,final_birth_date,sex,this.country_code_id,this.is_public ?? false,this.is_disable_person);
        else {
            var user = new User(this.id,this.password!,this.user_template.salt,this.name,this.user_template.level,this.user_template.admin_since_date,this.user_template.inactive_date,this.user_template.user_who_inactivated_account,this.email,final_birth_date,sex,this.country_code_id,this.user_template.account_creation,this.is_public ?? false,this.is_disable_person,this.user_template.bus_pass_id,this.user_template.bus_pass_valid_from,this.user_template.bus_pass_valid_until);
            
            Console.WriteLine($"OLD PASSWORD {this.password}");
            if (this.new_password == true)
                user.reset_password(this.password!);
            Console.WriteLine($"NEW PASSWORD {this.password}");
            return user;
        }

    }

}