using ErrorOr;

namespace MimoContacts.ServiceErrors;

public static class Errors
{
    public static class Contact
    {
        public static Error NotFound =>
            Error.NotFound(
                code: "Contact.NotFound",
                description: "Contact not found"
            );

        public static Error InvalidFirstName =>
            Error.Validation(
                code: "Contact.InvalidFirstName",
                description: "Contact first name must be at least "
                    + Models.Contact.MinFirstNameLength
                    + " characters long"
            );

        public static Error InvalidLastName =>
            Error.Validation(
                code: "Contact.InvalidLastName",
                description: "Contact last name must be at least "
                    + Models.Contact.MinFirstNameLength
                    + " characters long"
            );

        public static Error InvalidEmail =>
            Error.Validation(
                code: "Contact.InvalidEmail",
                description: "Contact email is invalid"
            );

        public static Error InvalidPhoneNumber =>
            Error.Validation(
                code: "Contact.InvalidPhoneNumber",
                description: "Contact phone number is invalid"
            );

        public static Error InvalidPassword =>
            Error.Validation(
                code: "Contact.InvalidPassword",
                description: "The password should not be null or empty,"
                    + "be at least 8 characters long,"
                    + "contain at least one uppercase letter,"
                    + "contain at least one lowercase letter,"
                    + "contain at least one digit"
            );

        public static Error InvalidCategory =>
            Error.Validation(
                code: "Contact.InvalidCategory",
                description: "Contact category must be "
                    + Models.Contact.ContactCategory.Business.ToString() + " or "
                    + Models.Contact.ContactCategory.Private.ToString() + " or "
                    + Models.Contact.ContactCategory.Other.ToString()
            );
    }
}