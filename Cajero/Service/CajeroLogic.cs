using Cajero.DataAccess;
using Cajero.Models.DAO;
using Cajero.Models.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Cajero.Service
{
    public class CajeroLogic
    {
        private readonly CajeroAccess _cajeroAccess;
        private readonly IConfiguration _configuration;

        public CajeroLogic(CajeroAccess cajeroAccess, IConfiguration configuration)
        {
            _cajeroAccess = cajeroAccess;
            _configuration = configuration;
        }

        public async Task<bool> ValidarDatos(long numeroTarjeta, int pin)
        {
            var result = await _cajeroAccess.ValidarDatos(numeroTarjeta, pin);
            return result;
        }

        public string GenerateJwtToken(long numeroTarjeta)
        {
            byte[] keyBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("NumeroTarjeta", numeroTarjeta.ToString()),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Tarjeta> SearchTarjeta(long numeroTarjeta)
        {
            var result = await _cajeroAccess.SearchTarjeta(numeroTarjeta);
            return result;
        }
    }
}