using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials =>
            Error.Validation(code: "Auth.Credentials", description: "Invalid credentials.");

        public static Error TokenExpiredOrInvalid => Error.Validation(code: "Auth.TokenExpiredOrInvalid",
            description: "Token expired or invalid.");
    }
}