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

            CreateMap<Address, AddressDTO>();

            CreateMap<Company, CompanyDTO>();
            CreateMap<CreateCompany, Company>()
                .ForMember(c => c.Address, s => s.MapFrom( dto => new Address() { 
                    Country = dto.Country,
                    City = dto.City,
                    PostalCode = dto.PostalCode, 
                    Street = dto.Street 
                }));

            CreateMap<User, UserDTO>()
                .ForMember(dto=> dto.Team, s=> s.MapFrom(u=> u.Team.NameOfTeam));
            CreateMap<CreateUser, User>()
                .ForMember(u => u.Address,s=> s.MapFrom(dto => new Address{
                    Country = dto.Country,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                    Street = dto.Street
                }));

            CreateMap<CreateProject, Project>();
            CreateMap<Project, ProjectDTO>()
                .ForMember(dto => dto.ResponsibleTeam, s => s.MapFrom(p => p.OwnerTeam.NameOfTeam))
                .ForMember(dto => dto.UsersAssigned, s => s.MapFrom(p => p.UserProjects.Select(up => up.User)));
        }
    }
}
