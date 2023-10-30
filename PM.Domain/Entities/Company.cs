using ErrorOr;

namespace PM.Domain.Entities;

public class Company : BaseEntity
{
    private readonly List<Project> _orderedProjects = new();
    private readonly List<Project> _executedProjects = new();

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<Project> CustomerProjects => _orderedProjects.ToList();

    public IReadOnlyCollection<Project> ExecutedProjects => _executedProjects.ToList();

    private Company() { }

    internal Company(
        string name)
    {
        Name = name;
    }

    public static ErrorOr<Company> Create(string name)
    {
        return new Company(name);
    }
}
