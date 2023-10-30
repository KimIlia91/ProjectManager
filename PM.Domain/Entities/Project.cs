using PM.Domain.Common.Enums;

namespace PM.Domain.Entities;

public sealed class Project : BaseEntity
{
    private readonly List<Employee> _employees = new();

    public string Name { get; private set; } = null!;

    public Company CustomerCompany { get; private set; } = null!;

    public Company ExecutorCompany { get; private set; } = null!;

    public int ManagerId { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public PriorityEnum Priority { get; private set; }

    public IReadOnlyCollection<Employee> Employees => _employees.ToList();

    internal Project(
        string name,
        Company customerCompany,
        Company executorCompany,
        int managerId,
        DateTime startDate,
        DateTime endDate,
        PriorityEnum priority)
    {
        Name = name;
        CustomerCompany = customerCompany;
        ExecutorCompany = executorCompany;
        ManagerId = managerId;
        StartDate = startDate;
        EndDate = endDate;
        Priority = priority;
    }

    public static Project Create(
        string name,
        Company customerCompany,
        Company executorCompany,
        int managerId,
        DateTime startDate,
        DateTime endDate,
        PriorityEnum priority)
    {
        if (startDate > endDate)
            throw new ArgumentException();

        return new Project(
            name,
            customerCompany,
            executorCompany,
            managerId,
            startDate,
            endDate,
            priority);
    }

    public void AddEmployee(Employee employee) 
    {
        _employees.Add(employee);
    }

    public void RemoveEmployee(Employee employee)
    {
        _employees.Remove(employee);
    }

    public void ChangeManager(int managerId)
    {
        ManagerId = managerId;
    }

    public void ChangeStartDate(DateTime date)
    {
        StartDate = date;
    }

    public void ChangeEndDate(DateTime date)
    {
        EndDate = date;
    }

    public void ChangePriority(PriorityEnum priority)
    {
        Priority = priority;
    }
}
