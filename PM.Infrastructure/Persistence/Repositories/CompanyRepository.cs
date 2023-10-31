using PM.Application.Common.Interfaces.IRepositories;
using PM.Domain.Entities;

namespace PM.Infrastructure.Persistence.Repositories;

public sealed class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    private readonly ApplicationDbContext _context;

    public CompanyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
