using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.RequestModel.Authentication
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
