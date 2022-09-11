using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserWebAddress : Entity
    {
        public int UserId { get; set; }
        public string GithubAddress { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public UserWebAddress()
        {

        }

        public UserWebAddress(int id,int userId, string githubAddress) : this()
        {
            Id = id;
            UserId = userId;
            GithubAddress = githubAddress;
        }
    }
}
