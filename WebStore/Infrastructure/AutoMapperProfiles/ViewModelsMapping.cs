﻿using AutoMapper;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.AutoMapperProfiles
{
    public class ViewModelsMapping: Profile
    {
        public ViewModelsMapping()
        {
            CreateMap<Product, ProductViewModel>()
               .ForMember(view_model => view_model.Brand, opt => opt.MapFrom(product => product.Brand.Name))
               .ReverseMap();

            CreateMap<Employee, EmployeeViewModel>()
               .ForMember(view_model => view_model.Name, opt => opt.MapFrom(employee => employee.FirstName))
               .ReverseMap();
        }
    }
}
