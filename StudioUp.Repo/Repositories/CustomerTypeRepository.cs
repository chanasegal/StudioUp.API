using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class CustomerTypeRepository : ICustomerTypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerTypeRepository> _logger;

        public CustomerTypeRepository(DataContext context, IMapper mapper, ILogger<CustomerTypeRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CustomerTypeDTO>> GetAllAsync()
        {
            try
            {
                var customerTypes = await _context.CustomerTypes.Where(ct => ct.IsActive).ToListAsync();
                return _mapper.Map<List<CustomerTypeDTO>>(customerTypes);
            }
            catch 
            {
                throw;
            }
        }

        public async Task<CustomerTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                var customerType = await _context.CustomerTypes
                                                  .Where(ct => ct.ID == id && ct.IsActive)
                                                  .FirstOrDefaultAsync();
                if (customerType == null)
                {
                    throw new KeyNotFoundException("CustomerType with the specified ID does not exist.");
                }

                return _mapper.Map<CustomerTypeDTO>(customerType);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerTypeDTO> AddAsync(CustomerTypeDTO customerType)
        {
            try
            {
                var entity = _mapper.Map<CustomerType>(customerType);
                await _context.CustomerTypes.AddAsync(entity);
                await _context.SaveChangesAsync();
                return _mapper.Map<CustomerTypeDTO>(entity);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(CustomerTypeDTO customerType)
        {
            try
            {
                var existingCustomerType = await _context.CustomerTypes.FindAsync(customerType.ID);

                if (existingCustomerType == null)
                {
                    throw new KeyNotFoundException("CustomerType with the specified ID does not exist.");
                }

                _mapper.Map(customerType, existingCustomerType);
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
                var customerType = await _context.CustomerTypes.FindAsync(id);

                if (customerType == null)
                {
                    throw new KeyNotFoundException("CustomerType with the specified ID does not exist.");
                }

                if (!customerType.IsActive)
                {
                    throw new InvalidOperationException("CustomerType is already inactive.");
                }

                customerType.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
