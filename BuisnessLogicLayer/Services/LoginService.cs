using AutoMapper;
using BuisnessLogicLayer.Interfaces;
using BuisnessLogicLayer.Models;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using BuisnessLogicLayer.Helpers;
using NuGet.Protocol.Plugins;

namespace BuisnessLogicLayer.Services
{
    public class LoginService : ILoginService   
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var users = await _unitOfWork.UserRepository.GetAllWithNoTrackingAsync();

            var currentUserUnmapped = users.FirstOrDefault(x => x.UserName.ToLower()== loginRequest.UserName.ToLower() && x.UserStatus!= UserStatus.Banned);
            var currentUser = _mapper.Map<UserModel>(currentUserUnmapped);
            if(currentUser == null) {
                return null;
            }

            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, currentUser.PasswordSalt);
            if(currentUser.Password != passwordHash)
            {
                return null;
            }

            var token = await Task.Run(() => TokenHelper.GenerateToken(currentUser));

            return new LoginResponse { Username = currentUser.UserName, Id = currentUser.Id, Role = currentUser.Role.ToString(), Token = token };
        }



    }
}
