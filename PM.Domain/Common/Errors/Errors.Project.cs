using ErrorOr;

namespace PM.Domain.Common.Errors;

public static partial class Errors
{
    public static class Project
    {
        public static Error InvalidDate => Error.Validation(
          code: "",
          description: "InvalidDate");
    }
}
