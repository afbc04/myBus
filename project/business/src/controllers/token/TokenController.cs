using PacketHandlers;
using Token;
using Models;
using Nito.AsyncEx;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Controller {

    public class TokenController {

        public readonly AsyncReaderWriterLock Lock;
        private TokenModel _model;

        private static string secret = "tK9pZlJ+3zv7W5c0YVfW9pI+Hq5LsM6Yh1y+5x1Jx0U=";
        private static int expires_in_minutes = 60;

        public TokenController() {
            this.Lock = new();
            this._model = new TokenModel();
            TokenController.secret = Environment.GetEnvironmentVariable("JWT_TOKEN_SECRET") ?? TokenController.secret;
        }

        // ------------------ public methods ------------------

        public async Task<SendingPacket> create(TokenRequestWrapper request_wrapper) {

            if (request_wrapper.grant_type != "password")
                return new PacketFail("Token's grant type is not valid. Try 'password'",417);

            var auth_user = await this._model.get(request_wrapper.username);
            if (auth_user == null)
                return new PacketFail("User does not exists",404);

            if (Auth.verify_password(request_wrapper.password,auth_user.password,auth_user.salt) == false)
                return new PacketFail("Password is incorrect",403);

            string user_id = auth_user.ID;
            string user_level = User.get_level(auth_user.level);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenController.secret));
            var claims = new[] {
                new Claim("username", user_id),
                new Claim("level", user_level)
            };

            var access_token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(TokenController.expires_in_minutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var written_access_token = new JwtSecurityTokenHandler().WriteToken(access_token);

            var created_token = new CreatedToken(written_access_token,TokenController.expires_in_minutes,user_id,user_level);
            return new PacketSuccess(200, created_token.to_json());

        }

        public static (bool, AccessToken?, PacketFail?) validate_and_get_token(string token) {

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var token_handler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(TokenController.secret);

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
