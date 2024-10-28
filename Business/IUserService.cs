using CryptoTrade.Models;

namespace CryptoTrade.Business;

public interface IUserService
{
    public User RegisterUser(UserCreateDTO userCreateDTO);
    public IEnumerable<User> GetAllUsers();
    public User GetUserById(int userId);
    public User GetUserByEmail(string userEmail);
    //public void UpdateUser(int userId, User user); //UserUpdateDTO
    //public void DeleteUser(int userId);
}
