public class BusPass {

    public static readonly int idLength = 3;

    public string ID {get; set;}
    public double discount {get; set;}
    public short locality_level {get; set;}
    public int duration_days {get; set;}
    public bool is_active {get; set;}

    public BusPass(string ID, double discount, short locality_level, int duration_days, bool is_active) {
        this.ID = ID;
        this.discount = discount;
        this.locality_level = locality_level;
        this.duration_days = duration_days;
        this.is_active = is_active;
    }

    public IDictionary<string,object> to_json() {
        return new Dictionary<string,object> {
            ["id"] = this.ID,
            ["discount"] = this.discount,
            ["localityLevel"] = this.locality_level,
            ["duration"] = this.duration_days,
            ["active"] = this.is_active
        };
    }

}