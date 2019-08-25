using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Implementation.Services.Communication
{
    public class FundResponse : BaseResponse
    {
        public Fund Funds { get; private set; }

        private FundResponse(bool success, string message, Fund fund) : base(success, message)
        {
            Funds = fund;
        }

        public FundResponse(Fund fund) : this(true, string.Empty, fund) { }

        public FundResponse(string message) : this(false, message, null) { }
    }
}
