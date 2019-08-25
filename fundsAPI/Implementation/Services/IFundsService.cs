using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Services.Communication;

namespace funds_api.Implementation.Services
{
    public interface IFundsService
    {
        Task<IEnumerable<Fund>> ListAsync();
        Task<FundResponse> FindAsync(long id);
        Task<FundResponse> SaveAsync(Fund fund);
        Task<FundResponse> UpdateAsync(Fund fund);
        Task<FundResponse> DeleteAsync(Fund fund);
    }
}
