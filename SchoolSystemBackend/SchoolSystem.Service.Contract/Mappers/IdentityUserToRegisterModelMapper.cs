using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolSystem.Domain.Models.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Mappers
{
    public class IdentityUserToRegisterModelMapper : Profile
    {
        public IdentityUserToRegisterModelMapper()
        {
            CreateMap<IdentityUser, RegisterModel>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(dest => dest.UserName))
                .ForMember(src => src.Email, opt => opt.MapFrom(dest => dest.Email))
                .ReverseMap();
        }
    }
}
