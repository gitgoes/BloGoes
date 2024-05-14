using BloGoes.Domain.Admin;

namespace BloGoes.Services.JwtServices
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(Account account);
        public int? ValidateJwtToken(string? token);
    }
}
