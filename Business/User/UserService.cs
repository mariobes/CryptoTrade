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
        var registeredUserEmail = _repository.GetAllUsers().FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
        if (registeredUserEmail != null)
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        var registeredUserPhone = _repository.GetAllUsers().FirstOrDefault(u => u.Phone.Equals(dto.Phone, StringComparison.OrdinalIgnoreCase));
        if (registeredUserPhone != null)
        {
            throw new Exception("El teléfono ya está registrado.");
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

    public User GetUserByEmailOrPhone(string emailOrPhone)
    {
        var user = _repository.GetUserByEmailOrPhone(emailOrPhone);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario con email o teléfono {emailOrPhone} no encontrado");
        }
        return user;
    }

    public void UpdateUser(int userId, UserUpdateDto dto)
    {
        var user = GetUserById(userId);

        if (!string.IsNullOrWhiteSpace(dto.Email) && !dto.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase))
        {
            var registeredUserEmail = _repository.GetAllUsers()
                .FirstOrDefault(u => u.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase));
            if (registeredUserEmail != null)
            {
                throw new Exception("El correo electrónico ya está registrado.");
            }

            user.Email = dto.Email;
        }

        if (!string.IsNullOrWhiteSpace(dto.Phone) && !dto.Phone.Equals(user.Phone, StringComparison.OrdinalIgnoreCase))
        {
            var registeredUserPhone = _repository.GetAllUsers()
                .FirstOrDefault(u => u.Phone.Equals(dto.Phone, StringComparison.OrdinalIgnoreCase));
            if (registeredUserPhone != null)
            {
                throw new Exception("El teléfono ya está registrado.");
            }

            user.Phone = dto.Phone;
        }

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = PasswordHasher.Hash(dto.Password);
        }

        _repository.UpdateUser(user);
    }

    public void DeleteUser(int userId)
    {
        GetUserById(userId);
        _repository.DeleteUser(userId);
    }
}