using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Implementation.Repositories
{
    public interface IFundsRepository
    {
        Task<IEnumerable<Fund>> ListAsync();
        Task AddAsync(Fund fund);
        Task<Fund> FindByIdAsync(long id);
        void Update(Fund fund);
        void Remove(Fund fund);
    }
}
