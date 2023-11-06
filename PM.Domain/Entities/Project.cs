using ErrorOr;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Errors;

namespace PM.Domain.Entities;

/// <summary>
/// Represents a project entity.
/// </summary>
public sealed class Project : BaseEntity
{
    private readonly List<User> _users = new();
    private readonly List<Task> _tasks = new();

    /// <summary>
    /// Gets or sets the name of the project.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets or sets the name of the customer company.
    /// </summary>
    public string CustomerCompany { get; private set; }

    /// <summary>
    /// Gets or sets the name of the executor company.
    /// </summary>
    public string ExecutorCompany { get; private set; }

    /// <summary>
    /// Gets or sets the manager of the project.
    /// </summary>
    public User? Manager { get; private set; }

    /// <summary>
    /// Gets or sets the start date of the project.
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    /// Gets or sets the end date of the project.
    /// </summary>
    public DateTime EndDate { get; private set; }

    /// <summary>
    /// Gets or sets the priority of the project.
    /// </summary>
    public Priority Priority { get; private set; }

    /// <summary>
    /// Gets the collection of users associated with the project.
    /// </summary>
    public IReadOnlyCollection<User> Users => _users.ToList();

    /// <summary>
    /// Gets the collection of tasks associated with the project.
    /// </summary>
    public IReadOnlyCollection<Task> Tasks => _tasks.ToList();

    private Project() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    /// <param name="name">The name of the project.</param>
    /// <param name="customerCompany">The name of the customer company.</param>
    /// <param name="executorCompany">The name of the executor company.</param>
    /// <param name="manager">The manager of the project.</param>
    /// <param name="startDate">The start date of the project.</param>
    /// <param name="endDate">The end date of the project.</param>
    /// <param name="priority">The priority of the project.</param>
    internal Project(
        string name,
        string customerCompany,
        string executorCompany,
        User manager,
        DateTime startDate,
        DateTime endDate,
        Priority priority)
    {
        Name = name;
        CustomerCompany = customerCompany;
        ExecutorCompany = executorCompany;
        Manager = manager;
        StartDate = startDate;
        EndDate = endDate;
        Priority = priority;
        _users.Add(manager);
    }

    /// <summary>
    /// Creates a new project with the specified details.
    /// </summary>
    /// <param name="name">The name of the project.</param>
    /// <param name="customerCompany">The name of the customer company.</param>
    /// <param name="executorCompany">The name of the executor company.</param>
    /// <param name="manager">The manager of the project.</param>
    /// <param name="startDate">The start date of the project.</param>
    /// <param name="endDate">The end date of the project.</param>
    /// <param name="priority">The priority of the project.</param>
    /// <returns>An instance of the created project or an error.</returns>
    public static ErrorOr<Project> Create(
        string name,
        string customerCompany,
        string executorCompany,
        User manager,
        DateTime startDate,
        DateTime endDate,
        Priority priority)
    {
        if (startDate > endDate)
            return Errors.Project.InvalidDate;

        return new Project(
            name,
            customerCompany,
            executorCompany,
            manager,
            startDate,
            endDate,
            priority);
    }

    /// <summary>
    /// Updates the project with the specified details.
    /// </summary>
    /// <param name="name">The updated name of the project.</param>
    /// <param name="customerCompany">The updated name of the customer company.</param>
    /// <param name="executorCompany">The updated name of the executor company.</param>
    /// <param name="manager">The updated manager of the project.</param>
    /// <param name="startDate">The updated start date of the project.</param>
    /// <param name="endDate">The updated end date of the project.</param>
    /// <param name="priority">The updated priority of the project.</param>
    /// <returns>The updated project or an error.</returns>
    public ErrorOr<Project> Update(
        string name,
        string customerCompany,
        string executorCompany,
        User manager,
        DateTime startDate,
        DateTime endDate,
        Priority priority)
    {
        if (startDate > endDate)
            return Errors.Project.InvalidDate;

        Name = name;
        CustomerCompany = customerCompany;
        ExecutorCompany = executorCompany;
        Manager = manager;
        StartDate = startDate;
        EndDate = endDate;
        Priority = priority;

        return this;
    }

    /// <summary>
    /// Adds an employee to the project.
    /// </summary>
    /// <param name="employee">The user to add as an employee to the project.</param>
    public void AddUser(User employee)
    {
        _users.Add(employee);
    }

    /// <summary>
    /// Removes an employee from the project.
    /// </summary>
    /// <param name="employee">The user to remove from the project's employees.</param>
    public void RemoveEmployee(User employee)
    {
        _users.Remove(employee);
    }
}
