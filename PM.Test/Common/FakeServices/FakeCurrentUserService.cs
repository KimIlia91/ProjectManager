using PM.Application.Common.Interfaces.ISercices;
using PM.Test.Common.Constants;

namespace PM.Test.Common.FakeServices;

internal class FakeCurrentUserService : IDisposable, ICurrentUserService
{
    public int UserId => TestDataConstants.TestUserId;

    public bool IsSupervisor => true;

    public void Dispose()
    {
        this.Dispose();
    }
}
