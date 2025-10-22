namespace Token {

    public class AccessToken {

        public string username {get;}
        public string level {get;}

        public AccessToken(string username, string level) {
            this.username = username;
            this.level = level;
        }

        public static bool has_admin_perms(AccessToken? token) {

            if (token != null)
                return token.level == "administrator";

            return false;

        }

        public static bool has_driver_perms(AccessToken? token) {
            
            if (token != null)
                return token.level == "administrator" || token.level == "driver";

            return false;

        }

        public static bool has_traveller_perms(AccessToken? token) {
            return token != null;
        }

        public static bool has_admin_or_owner_perms(AccessToken? token, string ID) {
            
            if (token != null)
                return token.level == "administrator" || token.username == ID;

            return false;

        }

    }

    public class CreatedToken {

        public string access_token {get;}
        public int expires_in {get;}
        public string token_type {get;}

        public string ID {get;}
        public string level {get;}

        public CreatedToken(string token, int expires_in, string ID, string level) {
            this.access_token = token;
            this.expires_in = expires_in;
            this.token_type = "JWT";

            this.ID = ID;
            this.level = level;
        }

        public IDictionary<string,object> to_json() {
            return new Dictionary<string,object> {
                ["accessToken"] = this.access_token,
                ["user"] = new Dictionary<string,object>{
                    ["id"] = this.ID,
                    ["level"] = this.level,
                },
                ["expiresIn"] = this.expires_in,
                ["tokenType"] = this.token_type
            };
        }

    }

}
