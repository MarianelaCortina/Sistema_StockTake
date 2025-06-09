using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockTakeAPI.DTOs;
using StockTakeAPI.Helpers;
using StockTakeAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockTakeAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(SesionDTO sesion)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, sesion.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, sesion.Correo ?? ""),
                new Claim("nombreCompleto", sesion.NombreCompleto ?? ""),
                new Claim("rol", sesion.RolNombre ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
