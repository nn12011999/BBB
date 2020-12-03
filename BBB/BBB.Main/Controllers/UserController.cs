using BBB.Data.DataModel.Request;
using BBB.Data.DataModel.Response;
using BBB.Data.Entities;
using BBB.Main.Repositories;
using BBB.Main.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBB.Main.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly IUserServices _UserServices;
        public UserController(IUserRepository UserRepository,
            IUserServices UserServices)
        {
            _UserRepository = UserRepository;
            _UserServices = UserServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllUser()
        {
            return Ok(_UserRepository.GetAllUser());
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] AddUserRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }


            var UserQuery = _UserRepository.FindByName(request.UserName);
            if (UserQuery != null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User have been create"
                });
            }

            var User = new User()
            {
                UserName = request.UserName,
                Role = request.Role
            };

            var respone = _UserServices.AddUser(User);
            return Ok(respone);
        }

        [HttpPost("delete-user")]
        public IActionResult DeleteUser([FromBody] DeleteUserRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.UserId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var User = _UserRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var respone = _UserServices.DeleteUser(User);
            return Ok(respone);
        }

        [HttpPost("update-user")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "Please provide input information correctly."
                });
            }

            if (request.UserId <= 0)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var User = _UserRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            User.UserName = request.UserName;
            User.Role = request.Role;

            var respone = _UserServices.UpdateUser(User);
            return Ok(respone);
        }
    }
}
