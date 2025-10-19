using PacketHandlers;

public class CountryCodeRequestWrapper {

    public string id {get;set;}
    public string? name {get;set;}

    public CountryCodeRequestWrapper(string id, IDictionary<string,object> body) {
        this.id = id;
        this.name = (string?) PacketUtils.get_value(body,"name");
    }

    public void auto_fill(CountryCode cc) {
        this.id = this.id ?? cc.ID;
        this.name =  this.name ?? cc.name;
    }

    public CountryCode convert() {
        return new CountryCode(this.id.ToUpper(), this.name ?? "No name");
    }

}