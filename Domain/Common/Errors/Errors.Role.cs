using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
    public static class Role
    {
        public static Error NotExist => Error.NotFound(code: "Role.NotExist", description: "Role does not exist.");
    }
}