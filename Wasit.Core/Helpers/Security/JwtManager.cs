using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Wasit.Core.Helpers.Security
{
    public static class JwtManager
    {
        /// <summary>
        /// Get Principal From Token Without HttpContextAccessor
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static ClaimsPrincipal? GetPrincipal(string token, string? signature = null)
        {
            try
            {
                var secret = string.IsNullOrEmpty(signature)
                    ? Appsettings.GetSettingValue("JWT:SignatureKey")
                    : signature;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get Claims From Token Without HttpContextAccessor
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(string token)
        {
            try
            {
                ClaimsPrincipal? principal = GetPrincipal(token);
                if (principal != null)
                {
                    IEnumerable<Claim> claims = (principal.Identity as ClaimsIdentity).Claims;
                    return claims;
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Get Claim
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string? GetClaimValue(ClaimsIdentity identity, string type)
        {
            return identity.Claims.Where(c => c.Type == type).Select(c => c.Value).FirstOrDefault();
        }
    }
}
