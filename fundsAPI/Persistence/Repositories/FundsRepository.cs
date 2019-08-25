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
    public class FundsRepository : BaseRepository, IFundsRepository
    {
        public FundsRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Fund>> ListAsync()
        {
            return await _context.Funds.Include(p => p.Values).ToListAsync();
        }

        public async Task AddAsync(Fund fund)
        {
            await _context.Funds.AddAsync(fund);
        }

        public async Task<Fund> FindByIdAsync(long id)
        {
            return await _context.Funds.FindAsync(id);
        }

        public void Update(Fund fund)
        {
            _context.Funds.Update(fund);
        }

        public void Remove(Fund fund)
        {
            _context.Funds.Remove(fund);
        }
    }
}
