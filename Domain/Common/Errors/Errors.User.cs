using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail =>
            Error.Conflict(code: "User.DuplicateEmail",
                description: "Email is already in use.");

        public static Error NotExist => Error.NotFound(code: "User.NotExist", description: "User does not exist.");
    }
}