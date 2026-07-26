using System.Security.Claims;

namespace PrimeiroProjeto.Auth.Contract
{
    public interface ITokenGenerator
    {
        string GenerateAcessToken
            (IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken
            (string token);
    }
}
