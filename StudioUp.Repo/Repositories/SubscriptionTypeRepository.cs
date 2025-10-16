using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class SubscriptionTypeRepository : IRepository<SubscriptionTypeDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SubscriptionTypeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SubscriptionTypeDTO>> GetAllAsync()
        {

            try
            {
                var x = await _context.SubscriptionTypes.Where(y => y.IsActive).ToListAsync();
                return _mapper.Map<List<SubscriptionTypeDTO>>(x);
                          }
            catch 
            {
                throw;
            }

        }
        public async Task<SubscriptionTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                var s = await _context.SubscriptionTypes.FindAsync(id);
                if (s.IsActive)
                      return _mapper.Map<SubscriptionTypeDTO>(s);
                else
                    throw new Exception($"cant find SubscriptionType by ID {id}");
               
            }
            catch 
            {
                throw;
            }

        }
        public async Task<SubscriptionTypeDTO> AddAsync(SubscriptionTypeDTO subscriptionType)
        {
            try
            {
                var s = await _context.SubscriptionTypes.AddAsync(_mapper.Map<SubscriptionType>(subscriptionType));
                await _context.SaveChangesAsync();
                return subscriptionType;
            }
            catch
            {
                throw;
            }

        }
        public async Task UpdateAsync(SubscriptionTypeDTO subscriptionTypeDto)
        {
            try
            {
                _context.SubscriptionTypes.Update(_mapper.Map<SubscriptionType>(subscriptionTypeDto));
                await _context.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }

        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var subscriptionType = await _context.SubscriptionTypes.FindAsync(id);
                if (subscriptionType == null||subscriptionType.IsActive==false)
                {
                    throw new Exception($"cant find subscription by ID {id}");
                }
               subscriptionType.IsActive=false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

        }

    }
}
