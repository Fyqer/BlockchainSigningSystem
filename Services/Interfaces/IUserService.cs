using FCBlockchain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Services.Interfaces
{
    public interface IUserService
    {
        ResponseDTO Register(User USER);
        ResponseDTO Login(string login, string password);
        UsersDTO GetAll();
    }
}
