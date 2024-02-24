using AutoMapper;
using SchoolSystem.Domain.Models.AuthenticationModels;
using SchoolSystem.Service.Contract.DTO;
using SchoolSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Mappers
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>()
                .ReverseMap();
            CreateMap<UserViewModel, UserDTO>()
                .ReverseMap();
        }
    }
}
