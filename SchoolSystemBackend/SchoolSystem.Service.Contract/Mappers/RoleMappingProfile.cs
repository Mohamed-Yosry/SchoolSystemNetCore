using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SchoolSystem.Service.Contract.DTO;
using SchoolSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Mappers
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<IdentityRole, RoleDTO>()
                .ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>()
                .ReverseMap();
            CreateMap<RoleViewModel, RoleDTO>()
                .ReverseMap();
        }
    }
}
