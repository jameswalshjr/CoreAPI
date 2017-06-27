using System;
using AutoMapper;
using CoreAPI.Domain.Dto;
using CoreAPI.Domain.Entity;

namespace CoreAPI.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
            :this("SandBoxProfile")
        {

        }

        protected MappingProfile(string profileName)
            : base(profileName)
        {
            CreateMap<BillingItem, BillingItemEntity>();
        }
    }
}
