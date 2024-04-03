using AutoMapper;
using Data;
using DataLayer.DTOs.Users;
using DataLayer.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public JwtServices(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task InsertJWTToken(JwtDTO jwt)
        {
            var needToAdd = _mapper.Map<JWTModel>(jwt);
            if (needToAdd != null)
            {
                await _context.JWT!.AddAsync(needToAdd);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Loi: cap cuu!");
            }
        }
    }
}
