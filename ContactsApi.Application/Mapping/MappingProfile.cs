using AutoMapper;
using ContactsApi.Application.DTOs;
using ContactsApi.Domain.Models;

namespace ContactsApi.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateContactRequest, Contact>();
        CreateMap<UpdateContactRequest, Contact>();
    }
}