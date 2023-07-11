using ErrorOr;
using MimoContacts.Data;
using MimoContacts.Models;
using MimoContacts.ServiceErrors;

namespace MimoContacts.Services.Contacts;

public class ContactService : IContactService
{
    private readonly MimoContactsContext _context;

    public ContactService(MimoContactsContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> CreateContact(Contact contact)
    {
        _context.Contacts.Add(contact);

        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public ErrorOr<List<Contact>> GetAllContacts()
    {
        List<Contact> result = _context.Contacts.ToList();
        if (result.Count == 0)
        {
            return Errors.Contact.NotFound;
        }

        return result;
    }

    public async Task<ErrorOr<Contact>> GetContact(Guid id)
    {
        var findResult = await _context.Contacts.FindAsync(id);

        if (findResult == null)
        {
            return Errors.Contact.NotFound;
        }

        return findResult;
    }

    public async Task<ErrorOr<Updated>> UpdateContact(Contact contact)
    {
        var findResult = await _context.Contacts.FindAsync(contact.Id);

        if (findResult == null)
        {
            return Errors.Contact.NotFound;
        }
        else
        {
            _context.Contacts.Entry(findResult).CurrentValues.SetValues(contact);
        }

        await _context.SaveChangesAsync();
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> DeleteContact(Guid id)
    {
        var findResult = await _context.Contacts.FindAsync(id);

        if (findResult == null)
        {
            return Errors.Contact.NotFound;
        }

        _context.Contacts.Remove(findResult);

        await _context.SaveChangesAsync();
        return Result.Deleted;
    }
}