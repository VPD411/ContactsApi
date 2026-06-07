using ContactsApi.Application.Abstractions;
using ContactsApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApi.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _service;

    public ContactsController(IContactsService service)
    {
        _service = service;
    }

    // GET - Запрос на получение
    // GET: api/contacts
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll(CancellationToken cancellationToken)
    {
        // Возвращаем список всех контактов, полученных из таблицы Contacts
        return Ok(await _service.GetAllAsync(cancellationToken));
    }

    // GET - Запрос на получение
    // GET: api/contacts/{id}
    [HttpGet("{id::guid}")]
    public async Task<ActionResult<Contact>> Get(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(id, cancellationToken);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    // POST - Запрос на создание
    // POST: api/contacts
    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact, CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(contact, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    // PUT - Запрос на обновление
    // PUT: api/contacts/{id}
    [HttpPut("{id::guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Contact contact, CancellationToken cancellationToken)
    {
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