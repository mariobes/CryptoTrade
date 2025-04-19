using CryptoTrade.Data;
using CryptoTrade.Models;
using static CryptoTrade.Models.User;

namespace CryptoTrade.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(UserCreateDto dto)
    {
        var registeredUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        User user = new User
        {
            Name = dto.Name,
            Birthdate = dto.Birthdate.AddHours(2),
            Email = dto.Email,
            Password = PasswordHasher.Hash(dto.Password),
            Phone = dto.Phone,
            DNI = dto.DNI,
            Nationality = dto.Nationality
        };
        _userRepository.AddUser(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return user;
    }

    public User GetUserByEmail(string userEmail)
    {
        var user = _userRepository.GetUserByEmail(userEmail);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con email {userEmail} no encontrado");
        }
        return user;
    }

    public UserPreferencesDto GetUserPreferences(int userId)
    {
        var user = GetUserById(userId);
        return new UserPreferencesDto
        {
            Language = user.Language,
            Currency = user.Currency,
            Theme = user.Theme
        };
    }

    public void UpdateUserPreferences(int userId, string? language = null, string? currency = null, string? theme = null)
    {
        var user = GetUserById(userId);
        if (language != null) user.Language = language;
        if (currency != null) user.Currency = currency;
        if (theme != null) user.Theme = theme;
        _userRepository.UpdateUser(user);
    }

    public void UpdateUser(int userId, UserUpdateDto dto)
    {
        var user = GetUserById(userId);

        var registeredUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        user.Email = dto.Email;
        user.Password = dto.Password;
        user.Phone = dto.Phone;
        _userRepository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        GetUserById(userId);
        _userRepository.DeleteUser(userId);
    }
    
}