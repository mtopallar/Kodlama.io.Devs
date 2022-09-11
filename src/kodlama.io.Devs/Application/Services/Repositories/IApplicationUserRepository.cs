using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IApplicationUserRepository : IAsyncRepository<ApplicationUser>, IRepository<ApplicationUser>
    {
    }
}
