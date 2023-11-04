namespace PM.Application.Common.Models.Employee;

/// <summary>
/// 
/// </summary>
public class UserResult
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string MiddelName { get; set; } = string.Empty;
}