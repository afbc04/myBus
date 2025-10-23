public class UserBusPass {

    public DateTime valid_from { get; set; }
    public DateTime valid_until { get; set; }
    public BusPass bus_pass { get; set; }
    public string user_id {get; set;}

    public UserBusPass(string user_id, DateTime bus_pass_valid_from, DateTime bus_pass_valid_until, BusPass bus_pass) {

        this.user_id = user_id;
        this.valid_from = bus_pass_valid_from;
        this.valid_until = bus_pass_valid_until;
        this.bus_pass = bus_pass;

    }

    public IDictionary<string, object> to_json(bool includes_user) {

        var json = new Dictionary<string, object> {
            ["id"] = this.bus_pass.ID,
            ["validFrom"] = this.valid_from.ToString("yyyy-MM-dd"),
            ["validUntil"] = this.valid_until.ToString("yyyy-MM-dd"),
            ["discount"] = this.bus_pass.discount,
            ["localityLevel"] = this.bus_pass.locality_level,
            ["duration"] = this.bus_pass.duration_days,
            ["active"] = this.bus_pass.is_active
        };

        if (includes_user == true)
            json["userID"] = this.user_id;

        return json;

    }

}
