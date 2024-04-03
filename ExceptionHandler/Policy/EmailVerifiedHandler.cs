using ExceptionHandler.Policy.Requirement;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExceptionHandler.Policy
{
    public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Email && c.Value == "true"))
            {
                context.Succeed(requirement); // User's email is verified, so the requirement is met
            }
            return Task.CompletedTask;
        }
    }
}
