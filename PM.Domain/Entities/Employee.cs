using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace PM.Domain.Entities;

public sealed class Employee : IdentityUser<int>
{
    private readonly List<Project> _projects = new();
    private readonly List<Project> _manageProjects = new();
    private readonly List<Task> _executorTasks = new();
    private readonly List<Task> _authorTasks = new();

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? MiddelName { get; private set; }

    public IReadOnlyCollection<Project> Projects => _projects.ToList();

    public IReadOnlyCollection<Project> ManageProjects => _manageProjects.ToList();

    public IReadOnlyCollection<Task> ExecutorTasks => _executorTasks.ToList();

    public IReadOnlyCollection<Task> AuthorTasks => _authorTasks.ToList();

    private Employee() { }

    internal Employee(
        string firstName,
        string lastName,
        string? middleName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddelName = middleName;
        UserName = email;
        Email = email;
    }

    public static ErrorOr<Employee> Create(
        string firstName,
        string lastName,
        string email,
        string? middleName = null)
    {
        return new Employee(
            firstName, 
            lastName, 
            middleName, 
            email);
    }

    public void Update(
        string firstName,
        string lastName,
        string? middleName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddelName = middleName;
        Email = email;
    }

    public void AddProject(Project project)
    {
        _projects.Add(project);
    }

    public void AddExecutorTasks(Task task)
    {
        _executorTasks.Add(task);
    }

    public void AddAuthorTasks(Task task)
    {
        _authorTasks.Add(task);
    }
}
