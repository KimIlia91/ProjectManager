using ErrorOr;

namespace PM.Domain.Entities;

public class Employee : BaseEntity
{
    private readonly List<Project> _projects = new();
    private readonly List<Project> _manageProjects = new();
    private readonly List<Task> _executorTasks = new();
    private readonly List<Task> _authorTasks = new();

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? MiddleName { get; private set; }

    public string Email { get; private set; } = null!;

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
        MiddleName = middleName;
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

    public ErrorOr<Employee> Update(
        string firstName,
        string lastName,
        string email,
        string? middleName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        Email = email;

        return this;
    }

    public void AddProject(Project project)
    {
        UpdatedAt = DateTime.UtcNow;
        _projects.Add(project);
    }

    public void AddExecutorTasks(Task task)
    {
        UpdatedAt = DateTime.UtcNow;
        _executorTasks.Add(task);
    }

    public void AddAuthorTasks(Task task)
    {
        UpdatedAt = DateTime.UtcNow;
        _authorTasks.Add(task);
    }
}
