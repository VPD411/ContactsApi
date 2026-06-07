using ContactsApi.Application.Abstractions;
using ContactsApi.Domain.Models;
using ContactsApi.Infrastructures.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Application.Services;

public class ContactsService : IContactsService
{
    private readonly ContactsDbContext _context;

    public ContactsService(ContactsDbContext context)
    {
        _context = context;
    }

    #region Получение
    public async Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _context.Contacts.ToListAsync(cancellationToken);
        return result;
    }

    public async Task<Contact?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _context.Contacts.FindAsync([id], cancellationToken);
        return result;
    }
    #endregion


    #region Добавление
    public async Task<Contact> CreateAsync(Contact contact, CancellationToken cancellationToken)
    {
        var result = await _context.Contacts.AddAsync(contact, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
    #endregion

    #region Обновление
    public async Task<Contact?> UpdateAsync(Guid id, Contact newContact, CancellationToken cancellationToken)
    {
        var oldContact = await _context.Contacts.FindAsync([id], cancellationToken);
        if (oldContact == null)
        {
            return null;
        }

        oldContact.Phone = newContact.Phone;
        oldContact.Email = newContact.Email;
        oldContact.Name = newContact.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return oldContact;
    }
    #endregion

    #region Удаление
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts.FindAsync([id], cancellationToken);
        if (contact == null)
        {
            return;
        }

        _context.Remove(contact);

        await _context.SaveChangesAsync(cancellationToken);
    }
    #endregion

}
