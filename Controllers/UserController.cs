using FCBlockchain.Models;
using FCBlockchain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FCBlockchain.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService userService )
        {
            this.userService = userService;
        }
        [Route("api/User/getAll")]
        [HttpGet]
        public UsersDTO GetAll()
        {
            return userService.GetAll();
        }

        [Route("api/User/login")]
        [HttpGet]
        public ResponseDTO GetAll(string email, string password)
        {
            return userService.Login(email,password);
        }

        [Route("api/User/register")]
        [HttpGet]
        public ResponseDTO GetAll(User user)
        {
            return userService.Register(user);
        }
    }
}
