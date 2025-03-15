using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Asp.Versioning;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

/// <summary>
/// Аутентификация/авторизация пользователей
/// </summary>
[ApiVersion("1.0")]
[Route("[controller]")]
[ApiController]
public class UsersController(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManage,
    IConfiguration configuration) : ControllerBase
{
    /// <summary>
    /// Получение списка пользователей
    /// </summary>
    /// <returns>список пользователей</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IEnumerable<User> GetUsers()
    {
        return userManager.Users.ToList();
    }
    
    /// <summary>
    /// Получение токена для авторизации (доступно для любого пользователя)
    /// </summary>
    /// <param name="loginModel">Данные  для входа</param>
    /// <returns>token</returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var user = await userManager.FindByNameAsync(loginModel.UserName);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginModel.Password)) 
            return Unauthorized();
        
        var roles = await userManager.GetRolesAsync(user);
        var tokenSettings = configuration.GetSection("Token");
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.GetSection("Secret").Value));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: tokenSettings.GetSection("Issuer").Value,
            audience: tokenSettings.GetSection("Audience").Value,
            claims: new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName), 
                new(ClaimTypes.Email, user.Email), 
                new(ClaimTypes.Role, roles.FirstOrDefault())
            },
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        
        return Ok(new { Token = tokenString });
    }
    
    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="model">Данные пользователя</param>
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(CreateUserModel model)
    {
        var user = new User { Email = model.Email, UserName = model.UserName };
        if (await userManager.FindByNameAsync(user.UserName) != null)
            return BadRequest("User already exists");
       
        if (await roleManage.FindByNameAsync(model.Role) == null)
            await roleManage.CreateAsync(new IdentityRole(model.Role));

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
         
        await userManager.AddToRoleAsync(user, model.Role);
        return Ok();
    }
}