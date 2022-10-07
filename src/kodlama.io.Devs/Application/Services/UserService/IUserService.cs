using Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        public Task UserShouldExistWhenRequested(int userId);
    }
}
