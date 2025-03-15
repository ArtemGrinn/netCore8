using Microsoft.AspNetCore.Identity;
namespace Domain.Models;

/// <summary>
/// Пользователь
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Год рождения
    /// </summary>
    public int Year { get; set; }
} 