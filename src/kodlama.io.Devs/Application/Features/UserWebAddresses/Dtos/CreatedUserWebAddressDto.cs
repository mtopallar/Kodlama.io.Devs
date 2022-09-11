using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserWebAddresses.Dtos
{
    public class CreatedUserWebAddressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GithubAddress { get; set; }
    }
}
