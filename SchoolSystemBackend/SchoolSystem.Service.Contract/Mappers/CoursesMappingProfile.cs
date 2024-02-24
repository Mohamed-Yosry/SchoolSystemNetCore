using AutoMapper;
using SchoolSystem.Domain.Models;
using SchoolSystem.Service.Contract.DTO;
using SchoolSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Service.Contract.Mappers
{
    public class CoursesMappingProfile : Profile
    {
        public CoursesMappingProfile()
        {
            CreateMap<Course, CourseDTO>()
                .ReverseMap();
            CreateMap<CourseViewModel, CourseDTO>()
                .ReverseMap();
            CreateMap<CourseViewModel, Course>()
                .ReverseMap();
        }
    }
}
