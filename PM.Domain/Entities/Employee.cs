namespace PM.Domain.Entities;

public sealed class Employee : BaseEntity
{
    private readonly List<Project> _projects = new();
    private readonly List<Task> _tasks = new();

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? MiddleName { get; private set; }

    public string Email { get; private set; } = null!;

    public IReadOnlyCollection<Project> Projects => _projects.ToList();

    public IReadOnlyCollection<Task> Tasks => _tasks.ToList();

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

    public static Employee Create(
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

    public void AddProject(Project project)
    {
        _projects.Add(project);
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }
}
