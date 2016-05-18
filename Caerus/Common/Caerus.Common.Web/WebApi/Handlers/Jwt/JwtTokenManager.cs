using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.ServiceModel.Security.Tokens;

namespace Caerus.Common.Web.WebApi.Handlers.Jwt
{
    public class JwtTokenManager
    {
        public static string CreateJwtToken(string userName, string country)
        {
            var claimList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName),
                //new Claim(ClaimTypes.Authentication, password),
                new Claim(ClaimTypes.Country, country),
            };

            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };
            var sSKey = new InMemorySymmetricSecurityKey(SecurityConstants.KeyForHmacSha256);
            var jwtToken = tokenHandler.CreateToken(
                makeSecurityTokenDescriptor(sSKey, claimList));
            return tokenHandler.WriteToken(jwtToken);
        }

        public static ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler() {  RequireExpirationTime = true };

            // Parse JWT from the Base64UrlEncoded wire form 
            //(<Base64UrlEncoded header>.<Base64UrlEncoded body>.<signature>)
            var parsedJwt = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            var validationParams =
                new TokenValidationParameters()
                {
                    AllowedAudience = SecurityConstants.TokenAudience,
                    ValidIssuer = SecurityConstants.TokenIssuer,
                    ValidateIssuer = true,
                    SigningToken = new BinarySecretSecurityToken(SecurityConstants.KeyForHmacSha256),
                };

            return tokenHandler.ValidateToken(parsedJwt, validationParams);
        }

        public static string GetTokenValue(string jwtToken, string claimType)
        {
            if (claimType == ClaimTypes.Name)
                claimType = "unique_name";

            var tokenHandler = new JwtSecurityTokenHandler() { RequireExpirationTime = true };

            var parsedJwt = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            var claims = parsedJwt.Claims.GetEnumerator();
            while (claims.MoveNext())
            {
                if (claims.Current.Type == claimType)
                {
                    return claims.Current.Value;
                }
            }

            return string.Empty;
        }

        private static SecurityTokenDescriptor makeSecurityTokenDescriptor(
        InMemorySymmetricSecurityKey sSKey, List<Claim> claimList)
        {
            var now = DateTime.UtcNow;
            Claim[] claims = claimList.ToArray();
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                TokenIssuerName = SecurityConstants.TokenIssuer,
                AppliesToAddress = SecurityConstants.TokenAudience,
                Lifetime = new Lifetime(now, now.AddMinutes(SecurityConstants.TokenLifetimeMinutes)),
                SigningCredentials = new SigningCredentials(sSKey,
                    "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                    "http://www.w3.org/2001/04/xmlenc#sha256"),

            };
        }
    }

    public class SecurityConstants
    {
        public static readonly byte[] KeyForHmacSha256 = new byte[64];


        public static readonly string TokenIssuer = "Bearer";


        public static readonly string TokenAudience = "https://www.getbucks.com/api";


        public static readonly double TokenLifetimeMinutes = 180;

        public SecurityConstants()
        {
        }

    }
}
