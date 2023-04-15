using WebAuthExample.Models;

namespace WebAuthExample.Repositories
{
    public class IUsersRepository
    {
        public List<User> userList = new List<User>();

        public void SeedUserList()
        {
            userList.Add(new User
            {
                UserName = "Test",
                password = "test",
                Email = "Test@email.com",
                Role = "Admin",
            });
            userList.Add(new User
            {
                UserName = "test1",
                password = "test1",
                Email = "Test1@email.com",
                Role = "Reader"
            });
            userList.Add(new User
            {
                UserName = "test2",
                password = "test2",
                Email = "Test2@email.com",
                Role = "Admin",
            });
        }

        public List<User> GetUsers()
        {
            return userList.ToList();
        }

        public User? GetUser(string username, string password)
        {
            var user = userList.Where(n => n.UserName == username)
                .FirstOrDefault(p => p.password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public string CreateUser(User newUser)
        {
            try
            {
                userList.Add(newUser);
            }
            catch (Exception ex)
            {
                return $"Error adding user. Error: {ex.Message}";
            }
            return "User added successfully!";
        }
    }
}
