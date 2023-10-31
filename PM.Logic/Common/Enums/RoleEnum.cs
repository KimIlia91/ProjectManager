using System.ComponentModel;

namespace PM.Application.Common.Enums;

public enum RoleEnum
{
    [Description("Руководитель")] Supervisor,
    [Description("Менеджер")] Manager,
    [Description("Сотрудник")] Employee
}
