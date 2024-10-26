using CryptoTrade.Models;

namespace CryptoTrade.Data;

public interface IUserRepository
{
    public void AddUser(User user);
    public User GetUser(int userId);
    public User GetUserByEmail(string userEmail);
    public IEnumerable<User> GetAllUsers();
    public void DeleteUser(int userId);
    public void UpdateUser(User user);
    void SaveChanges();
}
