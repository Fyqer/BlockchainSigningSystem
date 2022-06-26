using FCBlockchain.Models;
using FCBlockchain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly FCBlockchainContext context;
        public UserService(FCBlockchainContext context)
        {
            this.context = context;
        }
        public UsersDTO GetAll()
        {
            var result = new UsersDTO() { UsersList = new List<User>() };
            result.UsersList = context.Users.ToList();
            return result;
        }
        public ResponseDTO Login(string login, string password)
        {
            var result = context.Users.Where(u => u.Email == login && u.Password == password);
            if (result.Any())
            {
                return new ResponseDTO { Code = "200", Status = "Success", Message = " Logged user" };

            }
            return new ResponseDTO { Code = "404", Status = "Not Found", Message = "Logged fail" };
        }

        public ResponseDTO Register(User user)
        {
            var result = context.Users.Where(u => u.Email == user.Email && u.Password == user.Password);
            if (result.Any())
            {
                return new ResponseDTO { Code = "500", Status = "Success", Message = "User exist IN DBN" };

            }
            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception EX)
            {
                return new ResponseDTO { Code = "500", Status = "exception occured", Message = EX.Message };

            }
            return new ResponseDTO { Code = "200", Status = "Success", Message = "uSER REGISTERED" };

        }

    }
}
