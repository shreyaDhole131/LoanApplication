using LoanApp_API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Application.Interfaces
{
    public interface IPendingDeals
    {
        List<PendingDealsDTO> fetchPendingDeals();
    }
}
