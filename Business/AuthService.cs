using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User CheckLogin(string email, string pasword)
        {
            User user = null;
            foreach (var userLogin in _repository.GetAllUsers())
            {
                if (userLogin.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                    userLogin.Password.Equals(pasword))
                {
                    user = userLogin;
                }
            }
            return user;
        }
     
    }
}
