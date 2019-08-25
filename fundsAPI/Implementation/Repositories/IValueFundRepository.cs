using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Implementation.Repositories
{
    public interface IValueFundRepository
    {
        Task<IEnumerable<ValueFund>> ListAsync();
        Task AddAsync(ValueFund valueFund);
        Task<ValueFund> FindByIdAsync(long id);
        void Update(ValueFund valueFund);
        void Remove(ValueFund valueFund);
    }
}
