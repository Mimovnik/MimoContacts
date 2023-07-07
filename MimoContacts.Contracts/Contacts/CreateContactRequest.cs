namespace MimoContacts.Contracts.Contacts;

public record CreateContactRequest(
    string Name,
    string Surname,
    string Email,
    string Password,
    string Category,
    string Subcategory,
    string PhoneNumber,
    DateTime birthDate
);