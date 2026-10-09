using AutoMapper;
using LoanApp_API.Application.DTO;
using LoanApp_API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Application.Mapper
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<LoanDeals, LoanDealDTO>().ReverseMap();
            CreateMap<LoanAccount, LoanAccountDto>().ForMember(
                    dest => dest.CustomerName,
                    opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName)
                );


        }
    }
}
