using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO dto);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public User GetUserByEmail(string userEmail);
    public UserPreferencesDTO GetUserPreferences(int userId);
    public void UpdateUserPreferences(int userId, string? language = null, string? currency = null, string? theme = null);
    public void UpdateUser(int userId, UserUpdateDTO dto);
    public void DeleteUser(int userId);
}
