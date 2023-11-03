using ErrorOr;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Errors;

namespace PM.Domain.Entities;

public sealed class Project : BaseEntity
{
    private readonly List<User> _employees = new();
    private readonly List<Task> _tasks = new();

    public string Name { get; private set; } = null!;

    public string CustomerCompany { get; private set; }

    public string ExecutorCompany { get; private set; }

    public User? Manager { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public Priority Priority { get; private set; }

    public IReadOnlyCollection<User> Employees => _employees.ToList();

    public IReadOnlyCollection<Task> Tasks => _tasks.ToList();

    private Project() { }

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
    }

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

    public void AddEmployee(User employee)
    {
        _employees.Add(employee);
    }

    public void RemoveEmployee(User employee)
    {
        _employees.Remove(employee);
    }

    public void ChangeManager(User manager)
    {
        Manager = manager;
    }

    public void ChangePriority(Priority priority)
    {
        Priority = priority;
    }

    public void AddTask(Task task)
    {
        _tasks.Add(task);
    }

    public void RemoveTask(Task task)
    {
        _tasks.Remove(task);
    }
}
