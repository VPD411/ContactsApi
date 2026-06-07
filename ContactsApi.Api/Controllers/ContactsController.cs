using ContactsApi.Domain.Models;
using ContactsApi.Infrastructures.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace ContactsApi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly ContactsDbContext _context;

    public ContactsController(ContactsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Contact>>> GetAll(CancellationToken cancellationToken)
    {
        return await _context.Contacts.ToListAsync(cancellationToken);
    }
}