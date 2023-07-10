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

    public async Task<ErrorOr<Contact>> GetContact(Guid id)
    {
        var findResult = await _context.Contacts.FindAsync(id);

        if (findResult == null)
        {
            return Errors.Contact.NotFound;
        }

        return findResult;
    }

    public async Task<ErrorOr<UpsertedContactResult>> UpsertContact(Contact contact)
    {
        var findResult = await _context.Contacts.FindAsync(contact.Id);

        bool IsNewlyCreated = findResult == null;

        if (IsNewlyCreated)
        {
            _context.Contacts.Add(contact);
        }
        else
        {
            findResult = contact;
        }

        await _context.SaveChangesAsync();
        return new UpsertedContactResult(IsNewlyCreated);
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