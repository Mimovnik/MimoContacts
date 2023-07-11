using System.Text.RegularExpressions;
using ErrorOr;
using MimoContacts.Contracts.Contacts;
using MimoContacts.ServiceErrors;

namespace MimoContacts.Models;

public class Contact
{
    public enum ContactCategory
    {
        Private,
        Business,
        Other,
    }

    public enum BusinessSubcategory
    {
        Boss,
        Client,
        Workmate,
    }

    public const int MinFirstNameLength = 3;
    public const int MinLastNameLength = 3;
    public const int MinPasswordLength = 8;

    public Guid Id { get; private set; }
    public DateTime LastModified { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public ContactCategory Category { get; private set; }
    public string Subcategory { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime BirthDate { get; private set; }

    private Contact(
        Guid id,
        DateTime lastModified,
        string firstName,
        string lastName,
        string email,
        string password,
        ContactCategory category,
        string subcategory,
        string phoneNumber,
        DateTime birthDate)
    {
        Id = id;
        LastModified = DateTime.SpecifyKind(lastModified, DateTimeKind.Utc);
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Category = category;
        Subcategory = subcategory;
        PhoneNumber = phoneNumber;
        BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
    }

    public static ErrorOr<Contact> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        string category,
        string subcategory,
        string phoneNumber,
        DateTime birthDate,
        Guid? id = null
    )
    {
        //Business logic
        List<Error> errors = new List<Error>();

        if (firstName.Length is < MinFirstNameLength)
        {
            errors.Add(Errors.Contact.InvalidFirstName);
        }

        if (lastName.Length is < MinLastNameLength)
        {
            errors.Add(Errors.Contact.InvalidLastName);
        }

        if (!IsValidEmail(email))
        {
            errors.Add(Errors.Contact.InvalidEmail);
        }

        if (!IsValidPassword(password))
        {
            errors.Add(Errors.Contact.InvalidPassword);
        }

        if (!IsValidPhoneNumber(phoneNumber))
        {
            errors.Add(Errors.Contact.InvalidPhoneNumber);
        }

        ContactCategory contactCategory = ContactCategory.Other;
        try
        {
            contactCategory = ParseCategory(category);
        }
        catch (ArgumentException)
        {
            errors.Add(Errors.Contact.InvalidCategory);
        }


        if (errors.Count > 0)
        {
            return errors;
        }

        return new Contact(
            id ?? Guid.NewGuid(),
            DateTime.UtcNow,
            firstName,
            lastName,
            email,
            password,
            contactCategory,
            subcategory,
            phoneNumber,
            birthDate
        );
    }

    public static bool IsValidEmail(string email)
    {
        // Regular expression pattern for email validation
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        // Check if the email matches the pattern
        Match match = Regex.Match(email, pattern);

        // Return true if the email is valid, false otherwise
        return match.Success;

    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Regular expression pattern for phone number validation
        string pattern = @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$";

        // Check if the phone number matches the pattern
        Match match = Regex.Match(phoneNumber, pattern);

        // Return true if the phone number is valid, false otherwise
        return match.Success;
    }

    public static bool IsValidPassword(string password)
    {
        // Check if the password meets the required criteria
        if (string.IsNullOrEmpty(password))
        {
            // Password should not be null or empty
            return false;
        }

        if (password.Length < MinPasswordLength)
        {
            // Password should be at least 8 characters long
            return false;
        }

        if (!ContainsUppercaseLetter(password))
        {
            // Password should contain at least one uppercase letter
            return false;
        }

        if (!ContainsLowercaseLetter(password))
        {
            // Password should contain at least one lowercase letter
            return false;
        }

        if (!ContainsDigit(password))
        {
            // Password should contain at least one digit
            return false;
        }

        // Password meets all the required criteria
        return true;
    }

    private static bool ContainsUppercaseLetter(string password)
    {
        return password.Any(char.IsUpper);
    }

    private static bool ContainsLowercaseLetter(string password)
    {
        return password.Any(char.IsLower);
    }

    private static bool ContainsDigit(string password)
    {
        return password.Any(char.IsDigit);
    }

    public static ContactCategory ParseCategory(string value)
    {
        return (ContactCategory)Enum.Parse(typeof(ContactCategory), value, true);
    }

    public static ErrorOr<Contact> From(Guid id, UpdateContactRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Category,
            request.Subcategory,
            request.PhoneNumber,
            request.BirthDate,
            id
        );
    }

    public static ErrorOr<Contact> From(CreateContactRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password,
            request.Category,
            request.Subcategory,
            request.PhoneNumber,
            request.BirthDate
        );
    }
}