public class BusPassFull {

    public static readonly int idLength = 3;

    public string ID {get; set;}
    public double discount {get; set;}
    public short locality_level {get; set;}
    public int duration_days {get; set;}
    public bool is_active {get; set;}
    public long users_active { get; set ;}
    public long users_expired { get; set ;}

    public BusPassFull(string ID, double discount, short locality_level, int duration_days, bool is_active, long users_active, long users_expired) {
        this.ID = ID;
        this.discount = discount;
        this.locality_level = locality_level;
        this.duration_days = duration_days;
        this.is_active = is_active;
        this.users_active = users_active;
        this.users_expired = users_expired;
    }

    public IDictionary<string,object> to_json() {
        var json = new Dictionary<string,object> {
            ["id"] = this.ID,
            ["discount"] = this.discount,
            ["localityLevel"] = this.locality_level,
            ["duration"] = this.duration_days,
            ["active"] = this.is_active
        };

        if (this.is_active)
            json["usersCount"] = new Dictionary<string,object> {
                    ["active"] = this.users_active,
                    ["expired"] = this.users_expired
                };

        return json;

    }

}