

using BuisnessLogicLayer.Helpers;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Requirements;

namespace WebAPI.Handlers
{
    public class UserBannedStatusHandler : AuthorizationHandler<UserStatusRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserStatusRequirement requirement)
        {

            var tmp = context.User.Claims.ToList();
            var claim = context.User.FindFirst(c => c.Type == "Status" && c.Issuer == TokenHelper.Issuer);
            if (!context.User.HasClaim(c => c.Type == "Status" && c.Issuer == TokenHelper.Issuer))
            {
                return Task.CompletedTask;
            }
            string? userStatus;
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            else
            {
                
                userStatus = context.User.FindFirst(c => c.Type == "Status" && c.Issuer == TokenHelper.Issuer).Value;
            }
            
            
            if (userStatus == requirement.UserStatus.ToString())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;


        }
    }
}
