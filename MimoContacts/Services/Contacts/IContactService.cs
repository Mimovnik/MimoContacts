using ErrorOr;
using MimoContacts.Models;

namespace MimoContacts.Services.Contacts;

public interface IContactService
{
    Task<ErrorOr<Created>> CreateContact(Contact contact);
    ErrorOr<List<Contact>> GetAllContacts();
    Task<ErrorOr<Contact>> GetContact(Guid id);
    Task<ErrorOr<UpsertedContactResult>> UpsertContact(Contact contact);
    Task<ErrorOr<Deleted>> DeleteContact(Guid id);
}