using Mapster;
using MapsterMapper;
using MediatR;
using PM.Infrastructure.Persistence;
using System.Reflection;

namespace PM.Tests.Common;

public abstract class TestCommandBase : IDisposable
{
    protected readonly ApplicationDbContext Context;
    protected readonly IMapper Mapper;

    public TestCommandBase()
    {
        Context = ApplicationDbContextFactory.Create();
        Mapper = new Mapper();
    }

    public void Dispose() => ApplicationDbContextFactory.Destroy(Context);
}
