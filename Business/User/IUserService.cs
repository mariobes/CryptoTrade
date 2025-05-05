using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDto dto);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public User GetUserByEmailOrPhone(string emailOrPhone);
    public void UpdateUser(int userId, UserUpdateDto dto);
    public void DeleteUser(int userId);
}
