using PacketHandlers;

public class TokenRequestWrapper {

    public string username {get;set;}
    public string password {get;set;}
    public string grant_type {get;set;}

    public TokenRequestWrapper(IDictionary<string,object> body) {
        this.username = (string) body["username"];
        this.password = (string) body["password"];
        this.grant_type = (string) body["grantType"];
    }

    //public Token convert() {
    //    return new Token(this.id.ToUpper(), this.name ?? "No name");
    //}

}