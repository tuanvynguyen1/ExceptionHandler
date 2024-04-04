using AutoMapper;
using Data;
using DataLayer.DTOs.Role;
using DataLayer.Interfaces;
using DataLayer.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public RoleServices(AppDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        

        public async Task<ServiceResponse<IList<RolesDTO>>> getAllJobByLevel(int level)
        { 
            ServiceResponse<IList<RolesDTO>> response = new ServiceResponse<IList<RolesDTO>>();
            try
            {
                var List =  _context.Roles.Where(x => x.RoleAccessLevel == level);
                
                response.Data = _mapper.Map<IList<RolesDTO>>(List); 

            } catch (Exception ex) { 
            }
            return response;



        }
    }
}
