using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : User
    {
        //sadece user ve UserWebAdress arasında 1-1 relation için kullanıldı. Bu entity zaten users tablosuna set edilecek şekilde ayarlandı.
        public virtual UserWebAddress UserWebAddress { get; set; }

        public ApplicationUser()
        {

        }
        
    }
}
