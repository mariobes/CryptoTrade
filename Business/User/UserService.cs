using CryptoTrade.Data;
using CryptoTrade.Models;
using static CryptoTrade.Models.User;

namespace CryptoTrade.Business;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User RegisterUser(UserCreateDto dto)
    {
        var registeredUser = _repository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
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
        _repository.AddUser(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _repository.GetAllUsers();
    }

    public User GetUserById(int userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con ID {userId} no encontrado");
        }
        return user;
    }

    public User GetUserByEmail(string userEmail)
    {
        var user = _repository.GetUserByEmail(userEmail);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con email {userEmail} no encontrado");
        }
        return user;
    }

    public void UpdateUser(int userId, UserUpdateDto dto)
    {
        var user = GetUserById(userId);

        var registeredUser = _repository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUser != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        user.Email = dto.Email;
        user.Password = dto.Password;
        user.Phone = dto.Phone;
        _repository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        GetUserById(userId);
        _repository.DeleteUser(userId);
    }
    
}