namespace PM.Domain.Entities;

public class Company : BaseEntity
{
    private readonly List<Project> _projects = new();

    public string Name { get; private set; } = null!;

    public IReadOnlyCollection<Project> Projects => _projects.ToList();

    internal Company(
        string name)
    {
        Name = name;
    }

    public static Company Create(string name)
    {
        return new Company(name);
    }
}
