using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDto dto);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public User GetUserByEmail(string userEmail);
    public UserPreferencesDto GetUserPreferences(int userId);
    public void UpdateUserPreferences(int userId, string? language = null, string? currency = null, string? theme = null);
    public void UpdateUser(int userId, UserUpdateDto dto);
    public void DeleteUser(int userId);
}
