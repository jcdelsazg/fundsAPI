using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace funds_api.Implementation.Models
{
    public class Fund
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ValueFund> Values { get; set; } = new List<ValueFund>(); 
    }
}
