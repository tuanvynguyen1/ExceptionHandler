using AutoMapper;
using DataLayer.DTOs.Users;
using DataLayer.Encryption;
using Entities;
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
            CreateMap<UsersModel, UserDTO>();

        }
    }
}
