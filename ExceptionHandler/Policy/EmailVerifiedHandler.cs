using Data;
using ExceptionHandler.Policy.Requirement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExceptionHandler.Policy
{
    
    public class EmailVerifiedHandler : AuthorizationHandler<EmailVerifiedRequirement>
    {
        private readonly AppDbContext _context;
        
        public EmailVerifiedHandler(AppDbContext context)
        {
            _context = context;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerifiedRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                string userid = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user =  _context.Users.FirstOrDefault(x => x.Id == int.Parse(userid));
                if(user.IsEmailConfirmed)
                    context.Succeed(requirement); // User's email is verified, so the requirement is met
            }
            return Task.CompletedTask;
        }
    }
}
