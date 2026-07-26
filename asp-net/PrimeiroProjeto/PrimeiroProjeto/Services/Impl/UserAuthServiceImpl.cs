using Microsoft.AspNetCore.Identity;
using PrimeiroProjeto.Auth.Contract;
using PrimeiroProjeto.Data.DTO.V1;
using PrimeiroProjeto.Model;
using PrimeiroProjeto.Repositories;

namespace PrimeiroProjeto.Services.Impl
{
    public class UserAuthServiceImpl : IUserAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthServiceImpl(
            IUserRepository repository,
            IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public User? FindByUserName(string username)
        {
            return _repository.FindByUsername(username);
        }
        public User Create(AccountCredentialsDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException
                    (nameof(dto));
            }
            var entity = new User
            {
                Username = dto.Username,
                FullName = dto.Fullname,
                Password = _passwordHasher.Hash
                (dto.Password),
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = null

            };
            return _repository.Create(entity);
        }


        public bool RevokeToken(string username)
        {
            var user=_repository
                .FindByUsername(username);
            if (user == null)
            {
                return false;
            }
            user.RefreshToken =null;
            _repository.Update(user);
            return true;
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
