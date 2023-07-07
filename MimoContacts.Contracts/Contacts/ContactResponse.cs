namespace MimoContacts.Contracts.Contacts;

public record ContactResponse(
    Guid Id,
    DateTime LastModified,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Category,
    string Subcategory,
    string PhoneNumber,
    DateTime BirthDate
);