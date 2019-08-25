using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;

namespace funds_api.Implementation.Services.Communication
{
    public class ValueFundResponse : BaseResponse
    {
        public ValueFund ValueFund { get; private set; }

        private ValueFundResponse(bool success, string message, ValueFund valueFund) : base(success, message)
        {
            ValueFund = valueFund;
        }

        public ValueFundResponse(ValueFund valueFund) : this(true, string.Empty, valueFund) { }

        public ValueFundResponse(string message) : this(false, message, null) { }
    }
}
