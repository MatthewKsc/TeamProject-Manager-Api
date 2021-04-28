using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject_Manager_Api.dao.Entitys;
using TeamProject_Manager_Api.Dtos.Models;
using TeamProject_Manager_Api.Dtos.Models_Operations;

namespace TeamProject_Manager_Api
{
    public class MappingProfile : Profile{

        public MappingProfile() {
            CreateMap<Company, CompanyDTO>();

            CreateMap<Address, AddressDTO>();

            CreateMap<CreatCompany, Company>()
                .ForMember(c => c.Address,
                    s => s.MapFrom(
                        dto => new Address() { 
                            Country = dto.Country, 
                            City = dto.City, 
                            PostalCode = dto.PostalCode, 
                            Street = dto.Street 
                         }));


        }
    }
}
