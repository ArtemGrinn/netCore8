using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Роли пользователей
/// </summary>
[ApiVersion("1.0")]
[Route("[controller]")]
[ApiController]
public class RolesController(RoleManager<IdentityRole> roleManage) : ControllerBase
{
    /// <summary>
    /// Получение списка ролей
    /// </summary>
    /// <returns>список ролей</returns>
    [HttpGet]
    public IEnumerable<IdentityRole> GetRoles()
    {
        return roleManage.Roles.ToList();
    }
}