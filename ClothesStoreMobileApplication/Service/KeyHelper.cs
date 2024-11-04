﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ClothesStoreMobileApplication.Service
{
    public class KeyHelper
    {
        public static RSA GetPrivateKey()
        {
            string relativePath = "Key/privateKey.pem";
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(basePath, relativePath);

            var rsa = RSA.Create();
            var privateKey = File.ReadAllText(fullPath);
            rsa.ImportFromPem(privateKey.ToCharArray());
            return rsa;
        }

        public static RSA GetPublicKey()
        {

            string relativePath = "Key/publicKey.pem";
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(basePath, relativePath);

            var rsa = RSA.Create();
            var publicKey = File.ReadAllText(fullPath);
            rsa.ImportFromPem(publicKey.ToCharArray());
            return rsa;
        }

        public static string GenerateJwtToken(string email, int otp)
        {
            var claims = new[]
            {
        new Claim("Email", email),
        new Claim("OTP", otp.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var rsa = GetPrivateKey();
            var creds = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: "RaiYugi",
                audience: "Saint",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static ClaimsPrincipal ValidateJwtToken(string token)
        {
            var rsa = GetPublicKey();
            var key = new RsaSecurityKey(rsa);

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "RaiYugi",
                    ValidAudience = "Saint",
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
