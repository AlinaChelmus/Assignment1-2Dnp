using System.Security.Claims;
using System.Text;
using System.Text.Json;
using BlazorApp.Auth;
using HTTPClient.ClientInterfaces;
using Microsoft.JSInterop;
using Shared.DTO;
using Shared.Models;


namespace BlazorApp1.Auth;

public class AuthImpl: IAuthManager
{
    private readonly HttpClient client = new ();
   
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!; // assigning to null! to suppress null warning.
    private readonly IUserService userService;
    private readonly IJSRuntime jsRuntime;
    
    public AuthImpl(IUserService userService, IJSRuntime jsRuntime)
    {
        this.userService = userService;
        this.jsRuntime = jsRuntime;
    }
    
    public async Task LoginAsync(string userName, string Password)
    {
        User? user = userService.ValidateUser(userName, Password); 

        ValidateLoginCredentials(Password, user); // Validate input data against data from database
        // validation success
        await CacheUserAsync(user!); // Cache the user object in the browser 

        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // convert user object to ClaimsPrincipal

        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task LogoutAsync()
    {
        await ClearUserFromCacheAsync(); // remove the user object from browser cache
        ClaimsPrincipal principal = CreateClaimsPrincipal(null); // create a new ClaimsPrincipal with nothing.
        OnAuthStateChanged?.Invoke(principal);
    }

    public async Task<ClaimsPrincipal> GetAuthAsync()
    {
        User? user =  await GetUserFromCacheAsync(); // retrieve cached user, if any

        ClaimsPrincipal principal = CreateClaimsPrincipal(user); // create ClaimsPrincipal

        return principal;
    }
    
    private async Task<User?> GetUserFromCacheAsync()
    {
        string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        if (string.IsNullOrEmpty(userAsJson)) return null;
        User user = JsonSerializer.Deserialize<User>(userAsJson)!;
        return user;
    }

    private static void ValidateLoginCredentials(string Password, User? user)
    {
        if (user == null)
        {
            throw new Exception("Username not found");
        }

        if (!string.Equals(Password,user.Password))
        {
            throw new Exception("Password incorrect");
        }
    }
    
    private static ClaimsPrincipal CreateClaimsPrincipal(User? user)
    {
        if (user != null)
        {
            ClaimsIdentity identity = ConvertUserToClaimsIdentity(user);
            return new ClaimsPrincipal(identity);
        }

        return new ClaimsPrincipal();
    }

    private async Task CacheUserAsync(User user)
    {
        string serialisedData = JsonSerializer.Serialize(user);
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
    }

    private async Task ClearUserFromCacheAsync()
    {
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
    }
    
    private static ClaimsIdentity ConvertUserToClaimsIdentity(User user)
    {
        // here we take the information of the User object and convert to Claims
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("Role", user.Role),
            
        };

        return new ClaimsIdentity(claims, "apiauth_type");
    }
    
}