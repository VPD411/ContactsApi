using ContactsApi.Application.DTOs;
using ContactsApi.Domain.Models;

namespace ContactsApi.Application.Mapping;

public static class ContactsMapper
{
    public static ContactResponse MapToResponse(Contact contact)
    {
        return new ContactResponse
        {
            Id = contact.Id,
            Name = contact.Name,
            Phone = contact.Phone,
            Email = contact.Email,
        };
    }


}