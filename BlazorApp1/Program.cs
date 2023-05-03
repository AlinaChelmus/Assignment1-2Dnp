using BlazorApp.Auth;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp1;
using BlazorApp1.Auth;
using HTTPClient.ClientInterfaces;
using HTTPClient.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IForumService, HttpForum>();
builder.Services.AddScoped<IAuthManager, AuthImpl>();
builder.Services.AddScoped<IUserService, HttpUser>();
await builder.Build().RunAsync();