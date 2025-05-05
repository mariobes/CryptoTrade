using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoTrade.Data;
using CryptoTrade.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static CryptoTrade.Models.User;

namespace CryptoTrade.Business
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public User CheckLogin(string emailOrPhone, string password)
        {
            User user = null;

            foreach (var userLogin in _repository.GetAllUsers())
            {
                if (userLogin.Email.Equals(emailOrPhone, StringComparison.OrdinalIgnoreCase) &&
                    PasswordHasher.Verify(password, userLogin.Password))
                {
                    user = userLogin;
                }

                if (userLogin.Phone.Equals(emailOrPhone) &&
                    PasswordHasher.Verify(password, userLogin.Password))
                {
                    user = userLogin;
                }
            }

            return user;
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["JWT:SecretKey"]; 
            var key = Encoding.ASCII.GetBytes(secretKey); 

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            if (!string.IsNullOrEmpty(user.Email))
                claims.Add(new Claim(ClaimTypes.Email, user.Email));

            if (!string.IsNullOrEmpty(user.Phone))
                claims.Add(new Claim(ClaimTypes.MobilePhone, user.Phone));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool HasAccessToResource(int? requestedUserID, string requestedUserEmailOrPhone, ClaimsPrincipal user) 
        {
            if (requestedUserID.HasValue)
            {
                var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
                { 
                    return false; 
                }
                var isOwnResource = userId == requestedUserID;

                var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;
                
                var hasAccess = isOwnResource || isAdmin;
                return hasAccess;
            }
            else if (!string.IsNullOrEmpty(requestedUserEmailOrPhone))
            {
                var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                var phoneClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone);

                var isOwnResource = (emailClaim?.Value.Equals(requestedUserEmailOrPhone, StringComparison.OrdinalIgnoreCase) ?? false)
                    || (phoneClaim?.Value.Equals(requestedUserEmailOrPhone, StringComparison.OrdinalIgnoreCase) ?? false);

                var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var isAdmin = roleClaim != null && roleClaim.Value == Roles.Admin;

                return isOwnResource || isAdmin;
            }
            return false;
        }
     
    }
}
