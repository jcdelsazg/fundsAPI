using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using funds_api.Implementation.Models;
using funds_api.Implementation.Resources;

namespace funds_api.Implementation.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveFundsResource, Fund>();

            CreateMap<ValueFundResource, ValueFund>();
        }
    }
}
