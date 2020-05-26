using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCoreReference.Profiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles()
        {

            CreateMap<Database.Customers, Models.CustomerOrdersDTO>()
                .ForMember(dest => dest.CustomerCity, opt => opt.MapFrom(src => src.City))
                .ReverseMap();
            
            CreateMap<Database.Orders, Models.OrderDTO>()
                .ForMember(dest => dest.CenaBrutto, opt => opt.MapFrom(src => src.Freight))
                .ReverseMap();
        }
    }
}
