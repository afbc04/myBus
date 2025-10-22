using System.Security.Cryptography;

public class UserFull {

    public bool is_active { get; set; }
    public bool closed_account_by_owner { get; set; }
    public int age { get; set; }
    public CountryCode? country_code { get; set; }
    public UserBusPass? bus_pass { get; set; }
    public double total_discount { get; set; }
    public User user { get; set; }

    public UserFull(User user, CountryCode? country_code, UserBusPass? bus_pass) {

        this.user = user;
        this.country_code = country_code;
        this.is_active = user.inactive_date == null;
        this.closed_account_by_owner = user.ID == user.user_who_inactivated_account;
        this.age = user.birth_date == null ? 0 : get_age((DateOnly) user.birth_date,DateOnly.FromDateTime(DateTime.Now));
        this.bus_pass = bus_pass;

        if (this.user.birth_date != null) {

            if (this.age < 12)
                this.total_discount = 100;
            else if (this.age < 18)
                this.total_discount = 80;
            else if (this.age < 24)
                this.total_discount = 40;

        }

        if (this.bus_pass != null)
            this.total_discount += this.bus_pass == null ? this.bus_pass!.bus_pass.discount : 0;
        
        this.total_discount = this.total_discount > 100 ? 100 : this.total_discount;

    }

    public IDictionary<string, object?> to_json() {

        var json = new Dictionary<string, object?>();

        json["id"] = this.user.ID;

        if (this.user.name != null)
            json["name"] = this.user.name;

        json["level"] = User.get_level(this.user.level);

        if (this.user.admin_since_date != null)
            json["adminSinceDate"] = this.user.admin_since_date?.ToString("yyyy-MM-dd");

        json["active"] = this.is_active;
        if (this.is_active != true) {
            json["inactiveDate"] = this.user.inactive_date?.ToString("yyyy-MM-dd");
            json["inactiveAccountUser"] = this.user.user_who_inactivated_account;
            json["closedAccountByOwner"] = this.closed_account_by_owner;
        }
        
        if (this.user.email != null)
            json["email"] = this.user.email;

        if (this.user.birth_date != null) {
            json["birthDate"] = this.user.birth_date?.ToString("yyyy-MM-dd");
            json["age"] = this.age;
        }

        json["sex"] = User.get_sex(this.user.sex);

        if (this.country_code != null)
            json["countryCode"] = this.country_code.to_json();

        json["accountCreation"] = this.user.account_creation.ToString("yyyy-MM-dd HH:mm:ss");
        json["public"] = this.user.is_public;

        if (this.user.is_disable_person != null)
            json["disablePerson"] = this.user.is_disable_person;

        if (this.bus_pass != null) {
            json["busPass"] = this.bus_pass.to_json(false);
        }

        json["totalDiscount"] = this.total_discount;

        return json;

    }

    private static int get_age(DateOnly birth_date, DateOnly date) {

        int age = date.Year - birth_date.Year;

        if (date < birth_date.AddYears(age))
            age--;

        return age;

    }


}
