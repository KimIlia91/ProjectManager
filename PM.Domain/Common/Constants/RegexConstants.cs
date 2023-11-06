namespace PM.Domain.Common.Constants;

public class RegexConstants
{
    public const string PasswordPolicyValidatorRegex = @"^(?=.*[!@#$%^&*])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,32}$";
    public const string Email = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
}
