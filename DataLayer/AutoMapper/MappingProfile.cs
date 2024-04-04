using AutoMapper;
using DataLayer.DTOs.Authentication;
using DataLayer.DTOs.Role;
using DataLayer.DTOs.Users;
using DataLayer.Encryption;
using Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.AutoMapper
{
    public class MappingProfile : Profile
    {
        private readonly IPasswordHasher _pwdHasher;
        public MappingProfile(IPasswordHasher pwdHasher)
        {
            _pwdHasher = pwdHasher;

            CreateMap<UserRegisterDTO, UsersModel>().ForMember(dest=>dest.Password, opt => opt.MapFrom(scr => _pwdHasher.Hash(scr.Password)));
            CreateMap<UsersModel, UserDTO>()
                 .ForMember(dto => dto.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(y => y.Role).ToList()));
            CreateMap<JwtDTO, JWTModel>().ForMember(dest => dest.TokenHashValue, opt => opt.MapFrom(src => _pwdHasher.Hash(src.Token)));
            //Reverse can map from 1->2 || 2->1
            CreateMap<UsersModel, UserInfoDTO>().ReverseMap(); 
            CreateMap<UsersModel, CredentialDTO>().ForMember(dto => dto.Roles, opt => opt.MapFrom(x => x.UserRoles.Select(y => y.Role).ToList())).ReverseMap();
            CreateMap<RoleModel, RolesDTO>().ReverseMap();
        }
    }
}
