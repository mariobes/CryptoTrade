using CryptoTrade.Data;
using CryptoTrade.Models;

namespace CryptoTrade.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User RegisterUser(UserCreateDTO dto)
    {
        var registeredUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        User user = new User
        {
            Name = dto.Name,
            Birthdate = dto.Birthdate,
            Email = dto.Email,
            Password = dto.Password,
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

    public void UpdateUser(int userId, UserUpdateDTO dto)
    {
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }

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
        var user = _userRepository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        _userRepository.DeleteUser(userId);
    }
    
}