using PM.Application.Common.Interfaces.ISercices;
using PM.Test.Common.Constants;

namespace PM.Test.Common.FakeServices;

internal class FakeCurrentUserIsSupervisorService : ICurrentUserService
{
    public int UserId => TestDataConstants.TestUserId;

    public bool IsSupervisor => true;
}
