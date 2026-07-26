using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Model;

namespace PrimeiroProjeto.Services
{
    public interface IUserAuthService
    {
        User? FindByUserName(string username);
        User Create(AccountCredentialsDTO
            dto);
        bool RevokeToken(string username);
        User Update(User user);
    }
}
