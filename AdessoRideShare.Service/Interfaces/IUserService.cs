﻿using AdessoRideShare.Db.Entity;
using AdessoRideShare.Model.RequestModel.Authentication;
using AdessoRideShare.Model.ResponseModel.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Service.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        LoginResponse Login(LoginRequest Request);

        CreateUserResponse CreateUser(CreateUserRequest Request);
    }
}
