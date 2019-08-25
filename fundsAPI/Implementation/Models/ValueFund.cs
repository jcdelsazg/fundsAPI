using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace funds_api.Implementation.Models
{
    public class ValueFund
    {
        public long Id { get; set; }

        public DateTime DateFund { get; set; }

        public int Value { get; set; }

        public long FundId { get; set; }
    }
}
