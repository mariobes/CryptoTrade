using CryptoTrade.Models;
using System.Text.Json;

namespace CryptoTrade.Data;

public class UserJsonRepository : IUserRepository
{
    private Dictionary<string, User> _users = new Dictionary<string, User>();
    private readonly string _filePath;
    private static int UserIdSeed { get; set; }

    public UserJsonRepository()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        _filePath = Path.Combine(basePath, "JsonData", "Users.json");

        if (File.Exists(_filePath))
        {
            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(jsonString);
                _users = users.ToDictionary(acc => acc.Id.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while reading the user file", e);
            }
        }

        UserIdSeed = _users.Any() ? _users.Values.Max(u => u.Id) + 1 : 1;
    }

    public void AddUser(User user)
    {
        user.Id = UserIdSeed++;
        _users[user.Id.ToString()] = user;
        SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _users.Values;
    }

    public User GetUser(int userId) => _users.FirstOrDefault(u => u.Value.Id == userId).Value;

    public User GetUserByEmailOrPhone(string emailOrPhone)
        => _users.FirstOrDefault(u => u.Value.Email == emailOrPhone || u.Value.Phone == emailOrPhone).Value;

    public void UpdateUser(User user)
    {
        _users[user.Id.ToString()] = user;
        SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        _users.Remove(userId.ToString());
        SaveChanges();
    }

    public void SaveChanges()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_users.Values, options);
            File.WriteAllText(_filePath, jsonString);
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while saving changes to the user file", e);
        }
    }
}