using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        //We need to load this when the application starts
        //to do this, open global.asax.cs and add Mapper.Initialize(c => c.AddProfile<MappingProfile>()) to the Application_Start method;
        public MappingProfile()
        {
            //domain to dto
            //creates a mapping configuration between 2 types
            //It takes 2 parameters, source and target
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<Genre, GenreDto>();

            //dto to domain
            //We also want to map a DTO to an object, and ignore overwriting the Id property
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<GenreDto, Genre>();

        }
    }
}