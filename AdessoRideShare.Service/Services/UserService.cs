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
    public class UserService : BaseService<User>, IUserService
    {
        private readonly TokenManagement _tokenManagement;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork,
            IOptions<TokenManagement> tokenManagement, IMapper mapper) : base(repository, unitOfWork)
        {
            _tokenManagement = tokenManagement.Value;
            _mapper = mapper;
        }

        public CreateUserResponse CreateUser(CreateUserRequest Request)
        {
            CreateUserResponse response = new CreateUserResponse();

            var anyUser = Search(x => x.UserName == Request.UserName).Any();
            if (anyUser)
                throw new Exception("Kullanıcı adı mevcut.");

            var user = _mapper.Map<User>(Request);
            Insert(user);
            _unitOfWork.Save();

            response.Message = "Kullanıcı oluşturulmuştur.";

            return response;
        }

        public LoginResponse Login(LoginRequest Request)
        {
            LoginResponse response = new LoginResponse();

            var user = Search(s => s.UserName == Request.UserName && s.Password == Request.Password).FirstOrDefault();
            if (user == null)
                throw new Exception("Kullanıcı adı veya şifre hatalı");

            var claim = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.RecordId.ToString())
                });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new SecurityTokenDescriptor
            {
                Subject = claim,
                Expires = DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(jwtToken);
            response.AuthToken = tokenHandler.WriteToken(token);

            return response;
        }
    }
}
