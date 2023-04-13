using System.Security.Claims;

namespace BlazorApp.Auth;

public interface IAuthManager
{
    public Task LoginAsync(string userName, string Password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}