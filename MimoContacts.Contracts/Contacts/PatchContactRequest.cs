namespace MimoContacts.Contracts.Contacts;

public record PatchContactRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password,
    string? Category,
    string? Subcategory,
    string? PhoneNumber,
    DateTime? BirthDate
);