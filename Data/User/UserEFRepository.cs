using CryptoTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrade.Data
{
    public class UserEFRepository : IUserRepository
    {
        private readonly CryptoTradeContext _context;

        public UserEFRepository(CryptoTradeContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            SaveChanges();
        }

        public IEnumerable<User> GetAllUsers() 
        {
            var users = _context.Users;
            return users;
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(user => user.Id == userId);
            return user;
        }

        public User GetUserByEmailOrPhone(string emailOrPhone)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == emailOrPhone || user.Phone == emailOrPhone);
            return user;
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            SaveChanges();
        }

        public void DeleteUser(int userId) 
        {
            var user = GetUser(userId);
            _context.Users.Remove(user);
            SaveChanges();
        }
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }   
}