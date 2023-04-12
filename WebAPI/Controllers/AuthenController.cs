using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;
using WebAPI.Services;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthenController: ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthenService authenService;

    public AuthenController(IConfiguration config, IAuthenService authenService)
    {
        this.config = config;
        this.authenService = authenService;
    }
    
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim("Username",user.UserName),
            new Claim("Password",user.Password),
            new Claim("Role",user.Role),
            new Claim("Id",user.Id.ToString())
        };
        return claims.ToList();
    }
/*
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
       
       
    }

    [HttpPost, Route("Login")]
    public async Task<ActionResult> Login([FromBody] UserCreationDTO userCreationDto)
    {
        try
        {
            User user = await authenService.ValidateUser(userCreationDto.UserName, userCreationDto.Password);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }*/
}