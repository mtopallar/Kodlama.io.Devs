using Application.Features.SubTechnologies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubTechnologies.Models
{
    public class SubTechnologyListModel:BasePageableModel
    {
        public IList<SubTechnologyListDto> Items { get; set; }
    }
}
