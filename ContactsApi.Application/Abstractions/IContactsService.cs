using ContactsApi.Domain.Models;

namespace ContactsApi.Application.Abstractions
{
    public interface IContactsService
    {
        Task<Contact> CreateAsync(Contact contact, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Contact>> GetAllAsync(CancellationToken cancellationToken);
        Task<Contact?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Contact?> UpdateAsync(Guid id, Contact newContact, CancellationToken cancellationToken);
    }
}