using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MimoContacts.Contracts.Contacts;
using MimoContacts.Models;
using MimoContacts.Services.Contacts;

namespace MimoContacts.Controllers;

public class ContactsController : ApiController
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost]
    public IActionResult Create(CreateContactRequest request)
    {
        ErrorOr<Contact> toModelResult = Contact.From(request);

        if (toModelResult.IsError)
        {
            return Problem(toModelResult.Errors);
        }

        var contact = toModelResult.Value;

        ErrorOr<Created> result = _contactService.CreateContact(contact).Result;

        return result.Match(
            created => CreatedAtGet(contact),
            errors => Problem(errors));
    }

    [HttpGet]
    public IActionResult Get()
    {
        ErrorOr<List<Contact>> result = _contactService.GetAllContacts();

        return result.Match(
            contacts => Ok(MapContactsResponse(contacts)),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get(Guid id)
    {
        ErrorOr<Contact> result = _contactService.GetContact(id).Result;

        return result.Match(
            contact => Ok(MapContactResponse(contact)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, UpdateContactRequest request)
    {
        ErrorOr<Contact> toModelResult = Contact.From(id, request);

        if (toModelResult.IsError)
        {
            return Problem(toModelResult.Errors);
        }

        var contact = toModelResult.Value;

        ErrorOr<Updated> result = _contactService.UpdateContact(contact).Result;
        return result.Match(
            updated => Ok(),
            errors => Problem(errors));
    }

    [HttpPatch("{id:guid}")]
    public IActionResult Patch(Guid id, PatchContactRequest request)
    {
        ErrorOr<Contact> findResult = _contactService.GetContact(id).Result;

        if (findResult.IsError)
        {
            return Problem(findResult.Errors);
        }

        var original = findResult.Value;
        ErrorOr<Contact> toModelResult = Contact.From(id, request, original);

        if (toModelResult.IsError)
        {
            return Problem(toModelResult.Errors);
        }

        var patched = toModelResult.Value;

        ErrorOr<Updated> result = _contactService.UpdateContact(patched).Result;
        return result.Match(
            updated => Ok(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        ErrorOr<Deleted> result = _contactService.DeleteContact(id).Result;

        return result.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
    }

    private List<ContactResponse> MapContactsResponse(List<Contact> contacts)
    {
        return contacts.Select(MapContactResponse).ToList();
    }

    private ContactResponse MapContactResponse(Contact contact)
    {
        return new ContactResponse(
            contact.Id,
            contact.LastModified,
            contact.FirstName,
            contact.LastName,
            contact.Email,
            contact.Password,
            contact.Category.ToString(),
            contact.Subcategory,
            contact.PhoneNumber,
            contact.BirthDate
        );
    }

    private CreatedAtActionResult CreatedAtGet(Contact contact)
    {
        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new { id = contact.Id },
            value: MapContactResponse(contact));
    }
}