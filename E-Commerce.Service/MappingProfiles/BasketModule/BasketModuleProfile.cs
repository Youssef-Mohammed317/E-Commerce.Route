using AutoMapper;
using E_Commerce.Domian.Entites.BasketModule;
using E_Commerce.Shared.DTOs.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Implementation.MappingProfiles.BasketModule
{
    public class BasketModuleProfile : Profile
    {
        public BasketModuleProfile()
        {
            CreateMap<CustomerBasket, BasketDTO>()
                .ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>()
                .ReverseMap();
        }
    }
}
