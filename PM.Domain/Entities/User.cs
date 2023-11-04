using ErrorOr;
using Microsoft.AspNetCore.Identity;

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
    public void Update(
        string firstName,
        string lastName,
        string? middleName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;
    }

    // <summary>
    /// Adds a project to the user.
    /// </summary>
    /// <param name="project">The project to add.</param>
    public void AddProject(Project project)
    {
        _projects.Add(project);
    }

    /// <summary>
    /// Adds a task to the user's executor tasks.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void AddExecutorTasks(Task task)
    {
        _executorTasks.Add(task);
    }

    /// <summary>
    /// Adds a task to the user's author tasks.
    /// </summary>
    /// <param name="task">The task to add.</param>
    public void AddAuthorTasks(Task task)
    {
        _authorTasks.Add(task);
    }
}
