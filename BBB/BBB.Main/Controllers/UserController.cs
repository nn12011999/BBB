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
        private readonly IUserRepository _userRepository;
        private readonly IUserServices _userServices;
        public UserController(IUserRepository UserRepository,
            IUserServices UserServices)
        {
            _userRepository = UserRepository;
            _userServices = UserServices;
        }

        [HttpGet("get-all")]
        public IActionResult GetAllUser()
        {
            return Ok(_userRepository.GetAllUser());
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


            var UserQuery = _userRepository.FindByName(request.UserName);
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

            var response = _userServices.AddUser(User);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
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

            var User = _userRepository.FindById(request.UserId);
            if (User == null)
            {
                return BadRequest(new ErrorViewModel
                {
                    ErrorCode = "400",
                    ErrorMessage = "User not found"
                });
            }

            var response = _userServices.DeleteUser(User);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
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

            var User = _userRepository.FindById(request.UserId);
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

            var response = _userServices.UpdateUser(User);
            if (response != "OK")
            {
                return BadRequest("Can not execute. Plz contact admin");
            }
            return Ok(response);
        }

        [HttpGet("get-by-id")]
        public IActionResult GetUserById([FromBody] int Id)
        {
            var response = _userRepository.FindById(Id);
            if (response == null)
            {
                return BadRequest("User not found");
            }
            return Ok(response);
        }
    }
}
