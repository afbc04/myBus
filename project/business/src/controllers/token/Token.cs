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

        public CreatedToken(string token, int expires_in) {
            this.access_token = token;
            this.expires_in = expires_in;
            this.token_type = "JWT";
        }

    }

    public class TokenHandler {

        private static readonly string default_secret = "tK9pZlJ+3zv7W5c0YVfW9pI+Hq5LsM6Yh1y+5x1Jx0U=";
        private static readonly int expires_in_minutes = 60;


        public static CreatedToken create_access_token(string username, string level) {

            string secret = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET") ?? TokenHandler.default_secret;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var claims = new[] {
                new Claim("username", username),
                new Claim("level", level)
            };

            var access_token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenHandler.expires_in_minutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var written_access_token = new JwtSecurityTokenHandler().WriteToken(access_token);

            return new CreatedToken(written_access_token,TokenHandler.expires_in_minutes);

        }

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
