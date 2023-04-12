using System.Security.Claims;
using BlazorApp.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.Auth;

public class CustomAuthenticationStateProvider: AuthenticationStateProvider
{
    private readonly IAuthManager authManager;

    public CustomAuthenticationStateProvider(IAuthManager authManager)
    {
        this.authManager = authManager;
        authManager.OnAuthStateChanged += AuthStateChanged;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authManager.GetAuthAsync(); // get the user-as-ClaimsPrincipal from IAuthService
        return await Task.FromResult(new AuthenticationState(principal));
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }
}