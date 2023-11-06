using ErrorOr;
using PM.Domain.Common.Resources;

namespace PM.Domain.Common.Errors;

public static partial class Errors
{
    public static class Project
    {
        public static Error InvalidDate => Error.Validation(
          code: ErrorsResource.InvalidDate,
          description: nameof(ErrorsResource.InvalidDate));
    }
}
