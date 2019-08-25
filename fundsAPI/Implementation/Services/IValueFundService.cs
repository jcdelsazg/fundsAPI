using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Services.Communication;

namespace funds_api.Implementation.Services
{
    public interface IValueFundService
    {
        Task<IEnumerable<ValueFund>> ListAsync();
        Task<ValueFundResponse> FindAsync(long id);
        Task<ValueFundResponse> SaveAsync(ValueFund valueFund);
        Task<ValueFundResponse> UpdateAsync(ValueFund valueFund);
        Task<ValueFundResponse> DeleteAsync(ValueFund valueFund);
    }
}
