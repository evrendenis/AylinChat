using AylinChat.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AylinChat.Client.Authentication
{
    public class PAStateProvider : AuthenticationStateProvider
    {
        private static readonly Task<AuthenticationState> defaultAuthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        private readonly Task<AuthenticationState?> authenticatedStateTask = defaultAuthenticatedTask;

        public PAStateProvider(PersistentComponentState state)
        {
            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
                return;
            Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id!),
                new Claim(ClaimTypes.Email, userInfo.Email!),
                new Claim(ClaimTypes.Name, userInfo.FullName!)
                ];
            authenticatedStateTask = Task .FromResult
                (new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, nameof(PAStateProvider)))))!;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
       => authenticatedStateTask!;
    }
}
