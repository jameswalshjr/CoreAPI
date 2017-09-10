using System;
using AutoMapper;
using CoreAPI.Domain.Dto;
using CoreAPI.Domain.Entity;

namespace CoreAPI.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BillingItem, BillingItemEntity>();
            CreateMap<BillingItem, BillingItem>();
        }

    }
}
