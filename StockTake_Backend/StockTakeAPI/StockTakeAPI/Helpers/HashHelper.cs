using Microsoft.AspNetCore.Identity;

namespace StockTakeAPI.Helpers
{
    public class HashHelper
    {
        public static string HashearClave(string clavePlano)
        {
            var hasher = new PasswordHasher<string>();
            return hasher.HashPassword(null, clavePlano);
        }

        public static bool VerificarClave(string claveHasheada, string claveDigitada)
        {
            var hasher = new PasswordHasher<string>();
            var resultado = hasher.VerifyHashedPassword(null, claveHasheada, claveDigitada);
            return resultado == PasswordVerificationResult.Success;
        }
    }
}
