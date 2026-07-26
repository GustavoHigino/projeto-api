using PrimeiroProjeto.Data.DTO.V1;

namespace PrimeiroProjeto.Services
{
    public interface ILoginService
    {
        TokenDTO? ValidateCredentials
            (UserDTO user);
        TokenDTO? ValidateCredentials
            (TokenDTO token);
        bool RevokeToken(string username);
        AccountCredentialsDTO Create(
            AccountCredentialsDTO user);

    }
}
