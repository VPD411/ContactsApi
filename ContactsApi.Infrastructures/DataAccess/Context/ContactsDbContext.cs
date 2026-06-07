using ContactsApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Infrastructures.DataAccess.Context;

public class ContactsDbContext : DbContext
{
    public ContactsDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = Guid.NewGuid(), Name = "Иван Иванов", Phone = "+71231234567", Email = "ivan@example.com" },
            new Contact { Id = Guid.NewGuid(), Name = "Петр Петров", Phone = "+79269876543", Email = "petr@example.com" }
            );
    }
}
