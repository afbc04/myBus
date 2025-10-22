using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PacketHandlers;

namespace Token {

    public class AccessToken {

        public string username {get;}
        public string level {get;}

        public AccessToken(string username, string level) {
            this.username = username;
            this.level = level;
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
                ["access_token"] = this.access_token,
                ["user"] = new Dictionary<string,object>{
                    ["id"] = this.ID,
                    ["level"] = this.level,
                },
                ["expiresIn"] = this.expires_in,
                ["tokenType"] = this.token_type
            };
        }

    }

    public class TokenHandler {

        private static readonly string default_secret = "tK9pZlJ+3zv7W5c0YVfW9pI+Hq5LsM6Yh1y+5x1Jx0U=";
        private static readonly int expires_in_minutes = 60;


        public static (bool, AccessToken?, PacketFail?) validate(string token) {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var token_handler = new JwtSecurityTokenHandler();

            string secret = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET") ?? TokenHandler.default_secret;
            var key = Encoding.UTF8.GetBytes(secret);

            try {
                
                var validationParams = new TokenValidationParameters {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };

                var token_parameters = token_handler.ValidateToken(token, validationParams, out SecurityToken validated_token);

                var username = token_parameters.Claims.FirstOrDefault(c => c.Type == "username")!.Value;
                var level = token_parameters.Claims.FirstOrDefault(c => c.Type == "level")!.Value;

                return (true, new AccessToken(username,level),null);

            }
            catch (SecurityTokenExpiredException) {
                return (false, null, new PacketFail("Token is expired",401,null));
            }
            catch (SecurityTokenInvalidSignatureException) {
                return (false, null, new PacketFail("Token was modified",401,null));
            }
            catch {
                return (false, null, new PacketFail("Token is invalid",401,null));
            }

        }

    }

}
