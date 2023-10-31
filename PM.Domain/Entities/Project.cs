using ErrorOr;
using PM.Domain.Common.Enums;
using PM.Domain.Common.Errors;

namespace PM.Domain.Entities;

public class Project : BaseEntity
{
    private readonly List<Employee> _employees = new();
    private readonly List<Task> _tasks = new();

    public string Name { get; private set; } = null!;

    public string CustomerCompany { get; private set; }

    public string ExecutorCompany { get; private set; }

    public Employee Manager { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public ProjectPriority Priority { get; private set; }

    public IReadOnlyCollection<Employee> Employees => _employees.ToList();

    public IReadOnlyCollection<Task> Tasks => _tasks.ToList();

    private Project() { }

    internal Project(
        string name,
        string customerCompany,
        string executorCompany,
        Employee manager,
        DateTime startDate,
        DateTime endDate,
        ProjectPriority priority)
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
        Employee manager,
        DateTime startDate,
        DateTime endDate,
        ProjectPriority priority)
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
        Employee manager,
        DateTime startDate,
        DateTime endDate,
        ProjectPriority priority)
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

    public void AddEmployee(Employee employee)
    {
        _employees.Add(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        _employees.Remove(employee);
    }

    public void ChangeManager(Employee manager)
    {
        Manager = manager;
    }

    public ErrorOr<DateTime> ChangeStartDate(DateTime date)
    {
        if (date > EndDate)
            return Errors.Project.InvalidDate;

        StartDate = date;
        return StartDate;
    }

    public ErrorOr<DateTime> ChangeEndDate(DateTime date)
    {
        if (date < StartDate)
            return Errors.Project.InvalidDate;

        EndDate = date;
        return EndDate;
    }

    public void ChangePriority(ProjectPriority priority)
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
