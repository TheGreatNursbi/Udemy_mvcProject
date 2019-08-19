using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Proj.Models;
using Proj.Dtos;

namespace Proj.App_Start
{
    public class MappingFrofile : Profile
    {
        public MappingFrofile()
        {
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<CustomerDTO, Customer>().ForMember(c => c.Id, opt => opt.Ignore()); 
            Mapper.CreateMap<Movie, MovieDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();
            Mapper.CreateMap<MovieDTO, Movie>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MembershipType, MembershipTypeDTO>();
        }
    }
}