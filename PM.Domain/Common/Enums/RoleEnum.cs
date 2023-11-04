using PM.Domain.Common.Constants;
using System.ComponentModel;

namespace PM.Domain.Common.Enums;

/// <summary>
/// 
/// </summary>
public enum RoleEnum
{
    /// <summary>
    /// Represents the role of a supervisor.
    /// </summary>
    [Description(RoleConstants.Supervisor)] 
    Supervisor,

    /// <summary>
    /// Represents the role of a manager.
    /// </summary>
    [Description(RoleConstants.Manager)] 
    Manager,

    /// <summary>
    /// Represents the role of an employee.
    /// </summary>
    [Description(RoleConstants.Employee)] 
    Employee
}
