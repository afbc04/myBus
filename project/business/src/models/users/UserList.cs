public class UserList {

    public string ID { get; set; }
    public string? name { get; set; }
    public char level { get; set; }
    public string? country_code { get; set; }
    public bool is_active { get; set; }

    public UserList(string ID,string? name,char level,string? country_code,bool is_active) {

        this.ID = ID;
        this.name = name;
        this.level = level;
        this.country_code = country_code;
        this.is_active = is_active;

    }

    public IDictionary<string, object?> to_json() {

        var json = new Dictionary<string, object?>();

        json["id"] = this.ID;

        if (this.name != null)
            json["name"] = this.name;
        
        json["level"] = User.get_level(this.level);
        
        if (this.country_code != null)
            json["countryCode"] = this.country_code;

        json["active"] = this.is_active;

        return json;

    }

}
