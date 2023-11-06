using ErrorOr;
using PM.Domain.Common.Resources;

namespace PM.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error FirstNameRequired => Error.Validation(
            code: ErrorsResource.FirstNameRequired,
            description: nameof(ErrorsResource.FirstNameRequired));

        public static Error LastNameRequired => Error.Validation(
            code: ErrorsResource.LastNameRequired,
            description: nameof(ErrorsResource.LastNameRequired));

        public static Error InvalidEmail => Error.Validation(
            code: ErrorsResource.InvalidEmail,
            description: nameof(InvalidEmail));
    }
}
