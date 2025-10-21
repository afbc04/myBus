public class AuthUser {

    public string ID { get; set; }
    public string password { get; set; }
    public int salt { get; set; }
    public char level { get; set; }
    public bool active { get; set; }

    public AuthUser(string ID, string password, int salt, char level, bool active) {

        this.ID = ID;
        this.password = password;
        this.salt = salt;
        this.level = level;
        this.active = active;
    
    }

}
