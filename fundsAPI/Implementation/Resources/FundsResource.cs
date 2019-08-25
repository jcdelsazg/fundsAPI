using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Implementation.Resources
{
    public class FundsResource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ValueFund> Values { get; set; }
    }
}
