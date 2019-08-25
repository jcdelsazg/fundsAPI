using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace funds_api.Implementation.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
