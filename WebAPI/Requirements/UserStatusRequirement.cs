using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Requirements
{

    public class UserStatusRequirement : IAuthorizationRequirement
    {
        public UserStatus UserStatus { get; }


        public UserStatusRequirement(UserStatus userStatuS)
        {
            UserStatus= userStatuS;
        }

    }

}