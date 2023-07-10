using Microsoft.EntityFrameworkCore;
using MimoContacts.Models;

namespace MimoContacts.Data;

public class MimoContactsContext : DbContext
{
    public MimoContactsContext(DbContextOptions<MimoContactsContext> options)
        : base(options) { }

    public DbSet<Contact> Contacts { get; set; }
}