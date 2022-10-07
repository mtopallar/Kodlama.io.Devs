﻿using Application.Features.UserOperationClaims.Dtos;
using Core.Persistence.Paging;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Models
{
    public class UserOperationClaimListModel:BasePageableModel
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }        
        public IList<UserOperationClaimListDto> Items { get; set; }
    }
}
