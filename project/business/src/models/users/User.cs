using System.Security.Cryptography;

public class User {

    /*
        Level:
            A : administrator
            D : driver
            T : traveller

        Sex:
            M : Male
            F : Female
            N : Non-specified
    */

    public static readonly int nameMaxLength = 50;
    public static readonly int emailMaxLength = 30;
    public static readonly int idMaxLength = 15;
    public static readonly int passwordMinLength = 4;
    public static readonly int passwordMaxLength = 20;

    public string ID { get; set; }
    public string password { get; set; }
    public int salt { get; set; }
    public string? name { get; set; }
    public char level { get; set; }
    public DateOnly? admin_since_date { get; set; }
    public DateOnly? inactive_date { get; set; }
    public string? user_who_inactivated_account { get; set; }
    public string? email { get; set; }
    public DateOnly? birth_date { get; set; }
    public char sex { get; set; }
    public string? country_code { get; set; }
    public DateTime account_creation { get; set; }
    public bool is_public { get; set; }
    public bool? is_disable_person { get; set; }
    public string? bus_pass_id { get; set; }
    public DateOnly? bus_pass_valid_from { get; set; }
    public DateOnly? bus_pass_valid_until { get; set; }

    public User(
        string ID,
        string password,
        int salt,
        string? name,
        char level,
        DateOnly? admin_since_date,
        DateOnly? inactive_date,
        string? user_who_inactivated_account,
        string? email,
        DateOnly? birth_date,
        char sex,
        string? country_code_id,
        DateTime account_creation,
        bool is_public,
        bool? is_disable_person,
        string? bus_pass_id,
        DateOnly? bus_pass_valid_from,
        DateOnly? bus_pass_valid_until
    ) {

        this.ID = ID;
        this.password = password;
        this.salt = salt;
        this.name = name;
        this.level = level;
        this.admin_since_date = admin_since_date;
        this.inactive_date = inactive_date;
        this.user_who_inactivated_account = user_who_inactivated_account;
        this.email = email;
        this.birth_date = birth_date;
        this.sex = sex;
        this.country_code = country_code_id;
        this.account_creation = account_creation;
        this.is_public = is_public;
        this.is_disable_person = is_disable_person;
        this.bus_pass_id = bus_pass_id;
        this.bus_pass_valid_from = bus_pass_valid_from;
        this.bus_pass_valid_until = bus_pass_valid_until;        

    }

    public static User register_user(
        string ID,
        string password,
        string? name,
        bool should_be_admin,
        string? email,
        DateOnly? birth_date,
        char sex,
        string? country_code_id,
        bool is_public,
        bool? is_disable_person
    ) {

        int salt = RandomNumberGenerator.GetInt32(0,int.MaxValue);
        string password_hashed = Auth.get_password_hashed(password,salt);

        char level = 'T';
        DateOnly? admin_since_date = null;

        if (should_be_admin) {
            level = 'A';
            admin_since_date = DateOnly.FromDateTime(DateTime.Now);
        }

        return new User(
            ID,
            password_hashed,
            salt,
            name,
            level,
            admin_since_date,
            null,
            null,
            email,
            birth_date,
            sex,
            country_code_id,
            DateTime.Now,
            is_public,
            is_disable_person,
            null,
            null,
            null
        );

    }

    public void reset_password(string new_password) {
        this.password = Auth.get_password_hashed(new_password,this.salt);
    }

    public IDictionary<string, object?> to_json() {

        var json = new Dictionary<string, object?>();

        json["id"] = this.ID;

        if (this.name != null)
            json["name"] = this.name;

        json["level"] = User.get_level(this.level);

        if (this.admin_since_date != null)
            json["adminSinceDate"] = this.admin_since_date?.ToString("yyyy-MM-dd");

        if (this.inactive_date != null) {
            json["inactiveDate"] = this.inactive_date?.ToString("yyyy-MM-dd");
            json["inactiveAccountUser"] = this.user_who_inactivated_account;
        }
        
        if (this.email != null)
            json["email"] = this.email;

        if (this.birth_date != null) {
            json["birthDate"] = this.birth_date?.ToString("yyyy-MM-dd");
        }

        json["sex"] = User.get_sex(this.sex);

        if (this.country_code != null)
            json["countryCode"] = this.country_code;

        json["accountCreation"] = this.account_creation.ToString("yyyy-MM-dd HH:mm:ss");
        json["public"] = this.is_public;

        if (this.is_disable_person != null)
            json["disablePerson"] = this.is_disable_person;

        if (this.bus_pass_id != null) {
            json["busPassID"] = this.bus_pass_id;
        }

        if (this.bus_pass_valid_from != null) {
            json["busPassValidFrom"] = this.bus_pass_valid_from;
        }

        if (this.bus_pass_valid_until != null) {
            json["busPassValidUntil"] = this.bus_pass_valid_until;
        }

        return json;

    }

    public static string get_level(char level) {

        return level switch {
            'A' => "administrator",
            'D' => "driver",
            'T' => "traveller",
            _ => "unknown"
        };

    }

    public static string get_sex(char sex) {

        return sex switch {
            'M' => "male",
            'F' => "female",
            _ => "non specified"
        };

    }

}
