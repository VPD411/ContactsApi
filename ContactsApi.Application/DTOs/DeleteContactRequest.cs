using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Application.DTOs;

public class DeleteContactRequest
{
    [Required(ErrorMessage = "ID обязателен")]
    public Guid Id { get; set; }
}