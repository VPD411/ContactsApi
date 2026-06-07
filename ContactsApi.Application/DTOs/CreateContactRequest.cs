using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Application.DTOs;

public class CreateContactRequest
{
    [Required(ErrorMessage = "Имя обязательно")]
    [StringLength(50, ErrorMessage = "Максимум 50 символов")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string? Email { get; set; } = string.Empty;
}