using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OperationClaimService
{
    public interface IOperationClaimService
    {
        public Task OperationClaimShouldExistsWhenRequested(int operaionClaimId);
    }
}
