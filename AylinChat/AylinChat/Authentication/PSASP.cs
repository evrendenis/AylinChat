using ChatModels.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace AylinChat.Authentication
{
    public class PSASP : ServerAuthenticationStateProvider, IDisposable

    {
        private readonly PersistentComponentState state;
        private readonly IdentityOptions options;
        private readonly PersistingComponentStateSubscription subscription;
        private Task<AuthenticationState>? authenticationStateTask;

        public PSASP(PersistentComponentState persistentComponentState, IOptions<IdentityOptions> optionsAccessor )
        {
            state = persistentComponentState;
            options = optionsAccessor.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnpersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
         => authenticationStateTask = task;

        private async Task OnpersistingAsync()
        {
            if (authenticationStateTask != null)
            {
                var authenticationState = await authenticationStateTask;
                var principal = authenticationState.User;
                if (principal.Identity?.IsAuthenticated == true)
                {
                    var userId = principal.FindFirst(options.ClaimsIdentity.UserIdClaimType)?.Value;
                    var email = principal.FindFirst(options.ClaimsIdentity.EmailClaimType)?.Value;
                    var fullname = principal.Claims.Where(f => f.Type == ClaimTypes.Name).Last().Value;

                    if (userId != null && email != null && fullname != null)
                    {
                        state.PersistAsJson(nameof(UserInfo), new UserInfo
                        {
                            Id = userId,
                            FullName = fullname,
                            Email = email,
                        });
                    }
                }
            }
        }

        public void Dispose()
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
