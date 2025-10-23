public class CountryCode {

    public static readonly int nameMaxLength = 20;
    public static readonly int idLength = 3;

    public string ID {get; set;}
    public string name {get; set;}

    public CountryCode(string ID, string name) {
        this.ID = ID;
        this.name = name;
    }

    public IDictionary<string,object> to_json() {
        return new Dictionary<string,object> {
            ["id"] = this.ID,
            ["name"] = this.name
        };
    }

}