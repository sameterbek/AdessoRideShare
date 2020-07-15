using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.Model.RequestModel.Authentication;
using AdessoRideShare.Model.ResponseModel.Authentication;
using AdessoRideShare.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<LoginResponse> Login(LoginRequest Request)
        {
            return _userService.Login(Request);
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<CreateUserResponse> CreateUser(CreateUserRequest Request)
        {
            return _userService.CreateUser(Request);
        }

    }
}
