using AdessoRideShare.Db.Entity;
using AdessoRideShare.Model.Authentication;
using AdessoRideShare.Model.RequestModel.Authentication;
using AdessoRideShare.Model.ResponseModel.Authentication;
using AdessoRideShare.Repository.Interfaces;
using AdessoRideShare.Service.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AdessoRideShare.Service.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(IRepository<City> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
