using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ApplicationUserRepository : EfRepositoryBase<ApplicationUser, BaseDbContext>, IApplicationUserRepository
    {
        public ApplicationUserRepository(BaseDbContext context) : base(context)
        {

        }
    }
}
