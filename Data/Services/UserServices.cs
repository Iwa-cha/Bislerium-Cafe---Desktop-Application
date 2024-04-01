
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetCW.Data
{
    public class UserServices
    {

        private readonly List<User> _users = new()
        {
            new User()
            {
                UserName = "admin",
                Password = "admin",
                Role = "admin",
            },

            new User()
            {
                UserName = "staff",
                Password = "staff",
                Role = "staff"
            }
        };

        public List<User> GetUsers()
        {
            return _users;
        }


        public User LogIn(string userName, string password)
        {
            const string errorMessage = "Invalid username or password";

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username and password is required");
            }


            User user = _users.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            return user ?? throw new Exception(errorMessage);
        }

        
    }
}
