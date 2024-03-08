using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace AylinChat.Authentication
{
    internal static class IdentityComponentsEnpointsRouteBuiLderExtensions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEnpoints(this IEndpointRouteBuilder endpoints) 
        {
            var accountGroup = endpoints.MapGroup("/Account");
            accountGroup.MapPost("/Logout" , async(ClaimsPrincipal user, SignInManager<AppUser> singInManger) =>
            {
                await singInManger.SignOutAsync();
                return TypedResults.LocalRedirect("/");
            });
            return accountGroup;
        }
    }
}
