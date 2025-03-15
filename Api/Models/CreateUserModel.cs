using System.ComponentModel.DataAnnotations;
namespace Api.Models;

/// <summary>
/// Создание пользователя
/// </summary>
public class CreateUserModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required(ErrorMessage = "Поле UserName является обязательным")]
    public string UserName { get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required(ErrorMessage = "Поле Password является обязательным")]
    public string Password { get; set; }   
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required(ErrorMessage = "Поле Email является обязательным")]
    public string Email { get; set; }
    
    /// <summary>
    /// Роль пользователя
    /// </summary>
    [Required(ErrorMessage = "Поле Role является обязательным")]
    public string Role { get; set; }
} 