using ErrorOr;
using Microsoft.AspNetCore.Identity;
using PM.Domain.Common.Constants;
using PM.Domain.Common.Errors;
using System.Text.RegularExpressions;

namespace PM.Domain.Entities;

/// <summary>
/// Represents a user with identity information.
/// </summary>
public sealed class User : IdentityUser<int>
{
    private readonly List<Project> _projects = new();
    private readonly List<Project> _manageProjects = new();
    private readonly List<Task> _executorTasks = new();
    private readonly List<Task> _authorTasks = new();
    private readonly List<UserRole> _userRoles = new();

    /// <summary>
    /// Gets or private sets the first name of the user.
    /// </summary>
    public string FirstName { get; private set; } = null!;

    /// <summary>
    /// Gets or private sets the last name of the user.
    /// </summary>
    public string LastName { get; private set; } = null!;

    /// <summary>
    /// Gets or private sets the middle name of the user.
    /// </summary>
    public string? MiddleName { get; private set; }

    /// <summary>
    /// Gets a read only collection of projects associated with the user.
    /// </summary>
    public IReadOnlyCollection<Project> Projects => _projects.ToList();

    /// <summary>
    /// Gets a read only collection of projects managed by the user.
    /// </summary>
    public IReadOnlyCollection<Project> ManageProjects => _manageProjects.ToList();

    /// <summary>
    /// Gets a read only collection of tasks assigned to the user as an executor.
    /// </summary>
    public IReadOnlyCollection<Task> ExecutorTasks => _executorTasks.ToList();

    /// <summary>
    /// Gets a read only collection of tasks created by the user.
    /// </summary>
    public IReadOnlyCollection<Task> AuthorTasks => _authorTasks.ToList();

    /// <summary>
    /// Gets a read only collection of roles associated with the user.
    /// </summary>
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.ToList();

    private User() { }

    /// <summary>
    /// Initializes a new instance of the User class.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="middleName">The middle name of the user (optional).</param>
    /// <param name="email">The email address used as the username and email for the user.</param>
    internal User(
        string firstName,
        string lastName,
        string? middleName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        UserName = email;
        Email = email;
    }

    /// <summary>
    /// Creates a new user with the provided information.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="email">The email address used as the username and email for the user.</param>
    /// <param name="middleName">The middle name of the user (optional).</param>
    /// <returns>An error result containing the created user or an error message.</returns>
    public static ErrorOr<User> Create(
        string firstName,
        string lastName,
        string email,
        string? middleName = null)
    {
        if (string.IsNullOrEmpty(firstName))
            return Errors.User.FirstNameRequired;

        if (string.IsNullOrEmpty(lastName))
            return Errors.User.LastNameRequired;

        var regex = new Regex(RegexConstants.Email, RegexOptions.IgnoreCase);

        if (!regex.IsMatch(email))
            return Errors.User.InvalidEmail;

        return new User(
            firstName,
            lastName,
            middleName,
            email);
    }

    /// <summary>
    /// Updates the user's information with the provided data.
    /// </summary>
    /// <param name="firstName">The first name of the user.</param>
    /// <param name="lastName">The last name of the user.</param>
    /// <param name="middleName">The middle name of the user (optional).</param>
    /// <param name="email">The new email address for the user.</param>
    public ErrorOr<User> Update(
        string firstName,
        string lastName,
        string? middleName,
        string email)
    {
        if (string.IsNullOrEmpty(firstName))
            return Errors.User.FirstNameRequired;

        if (string.IsNullOrEmpty(lastName))
            return Errors.User.LastNameRequired;

        var regex = new Regex(RegexConstants.Email, RegexOptions.IgnoreCase);

        if (!regex.IsMatch(email))
            return Errors.User.InvalidEmail;

        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;

        return this;
    }
}
