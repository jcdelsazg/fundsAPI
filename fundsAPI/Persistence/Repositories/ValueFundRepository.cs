using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Repositories;
using funds_api.Persistence.Contexts;

namespace funds_api.Persistence.Repositories
{
    public class ValueFundRepository : BaseRepository, IValueFundRepository
    {
        public ValueFundRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<ValueFund>> ListAsync()
        {
            return await _context.Values.ToListAsync();
        }

        public async Task AddAsync(ValueFund valueFund)
        {
            await _context.Values.AddAsync(valueFund);
        }

        public async Task<ValueFund> FindByIdAsync(long id)
        {
            return await _context.Values.FindAsync(id);
        }

        public void Update(ValueFund valueFund)
        {
            _context.Values.Update(valueFund);
        }

        public void Remove(ValueFund valueFund)
        {
            _context.Values.Remove(valueFund);
        }
    }
}
