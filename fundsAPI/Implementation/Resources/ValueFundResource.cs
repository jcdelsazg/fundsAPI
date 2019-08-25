using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace funds_api.Implementation.Resources
{
    public class ValueFundResource
    {
        public long Id { get; set; }

        [Required]
        public DateTime DateFund { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Value { get; set; }

        [Range(0, long.MaxValue)]
        [Required]
        public long FundId { get; set; }
    }
}
