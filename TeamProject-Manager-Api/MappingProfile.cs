using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;

namespace TeamProject_Manager_Api
{
    public class MappingProfile : Profile{

        public MappingProfile() {
            CreateMap<Company, CompanyDTO>();

            CreateMap<Address, AddressDTO>();
        }
    }
}
