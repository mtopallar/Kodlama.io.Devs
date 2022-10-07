﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Dtos
{
    public class CreatedUserOperationClaimDto
    {
        public int Id { get; set; }        
        public string UserFirstName { get; set; } 
        public string UserLastName { get; set; }
        public string OperationClaimName { get; set; }
    }
}
