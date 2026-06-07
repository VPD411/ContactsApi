using AutoMapper;
using ContactsApi.Application.Abstractions;
using ContactsApi.Application.DTOs;
using ContactsApi.Application.Mapping;
using ContactsApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _service;
    private readonly IMapper _mapper;

    public ContactsController(IContactsService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    // GET - Запрос на получение
    // GET: api/contacts
    [HttpGet]
    public async Task<ActionResult<List<ContactResponse>>> GetAll(CancellationToken cancellationToken)
    {
        // Возвращаем список всех контактов, полученных из таблицы Contacts
        var result = await _service.GetAllAsync(cancellationToken);
        var mapped = result.Select(ContactsMapper.MapToResponse);
        return mapped.ToList();
    }

    // GET - Запрос на получение
    // GET: api/contacts/{id}
    [HttpGet("{id::guid}")]
    public async Task<ActionResult<ContactResponse>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        var mapped = ContactsMapper.MapToResponse(result);
        return Ok(mapped);
    }

    // POST - Запрос на создание
    // POST: api/contacts
    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContact([FromBody] CreateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);
        var result = await _service.CreateAsync(contact, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, ContactsMapper.MapToResponse(result));
    }

    // PUT - Запрос на обновление
    // PUT: api/contacts/{id}
    [HttpPut("{id::guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);
        var result = await _service.UpdateAsync(id, contact, cancellationToken);
        return NoContent();
    }

    // DELETE - Запрос на удаление
    // DELETE: api/contacts/{id}
    [HttpDelete("{id::guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}