using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Model.ResponseModel.Authentication
{
    public class LoginResponse : BaseResponse
    {
        public string AuthToken { get; set; }
    }
}
