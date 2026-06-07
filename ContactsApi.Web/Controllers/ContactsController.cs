using ContactsApi.Domain.Models;
using ContactsApi.Infrastructures.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ContactsDbContext _context;

    public ContactsController(ContactsDbContext context)
    {
        _context = context;
    }

    // GET - Запрос на получение
    // GET: api/contacts
    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll(CancellationToken cancellationToken)
    {
        // Возвращаем список всех контактов, полученных из таблицы Contacts
        return Ok(await _context.Contacts.ToListAsync(cancellationToken));
    }

    // GET - Запрос на получение
    // GET: api/contacts/{id}
    [HttpGet("{id::guid}")]
    public async Task<ActionResult<Contact>> Get(Guid id, CancellationToken cancellationToken)
    {
        // Пытаемся найти контакт
        var contact = await _context.Contacts.FindAsync([id], cancellationToken);

        // Если не нашли - 404 (не найдено)
        if (contact == null)
        {
            return NotFound();
        }

        // Нашли - возвращаем найденный
        return Ok(contact);
    }

    // POST - Запрос на создание
    // POST: api/contacts
    [HttpPost]
    public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact, CancellationToken cancellationToken)
    {
        // Получаем contact из тела HTTP запроса с помощью [FromBody] и добавляем
        await _context.Contacts.AddAsync(contact, cancellationToken);

        // Сохраняем изменения
        await _context.SaveChangesAsync(cancellationToken); // Обязательно при изменении данных в БД

        // Возвращает код 201 и ищет созданную в бд сущность
        return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
    }

    // PUT - Запрос на обновление
    // PUT: api/contacts/{id}
    [HttpPut("{id::guid}")]
    public async Task<ActionResult> Update(Guid id, [FromBody] Contact contact, CancellationToken cancellationToken)
    {
        // Если id запроса и id в contact не совпадает - плохой запрос (HTTP 400)
        if (id != contact.Id)
        {
            return BadRequest(); // HTTP 400
        }

        // Обновляем полученную сущность
        _context.Contacts.Update(contact);

        // Сохраняем изменения
        await _context.SaveChangesAsync(cancellationToken);

        // 204 - Всё ок, но ничего не возвращаем
        return NoContent(); // HTTP 204
    }


    // DELETE - Запрос на удаление
    // DELETE: api/contacts/{id}
    [HttpDelete("{id::guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        // Пытаемся найти контакт
        var contact = await _context.Contacts.FindAsync([id], cancellationToken);

        // Если не нашли - 404
        if (contact == null)
        {
            return NotFound();
        }

        // Убираем элемент из бд
        _context.Contacts.Remove(contact);

        // Сохраняем изменения
        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}