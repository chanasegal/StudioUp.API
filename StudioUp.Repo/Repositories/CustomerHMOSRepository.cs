using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class CustomerHMOSRepository : ICustomerHMOSRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomerHMOSRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerHMOSDTO>> GetAllAsync()
        {
            try
            {
                var customersHMOs = await _context.CustomerHMOS.Where(ct => ct.IsActive).ToListAsync();
                return _mapper.Map<IEnumerable<CustomerHMOSDTO>>(customersHMOs);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerHMOSDTO> GetByIdAsync(int id)
        {
            try
            {
                var customerHMOS = await _context.CustomerHMOS
                    .Where(ct => ct.ID == id && ct.IsActive)
                    .FirstOrDefaultAsync();

                if (customerHMOS == null)
                {
                    throw new Exception($"CustomerHMO with ID {id} does not exist or is inactive.");
                }

                return _mapper.Map<CustomerHMOSDTO>(customerHMOS);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> AddAsync(CustomerHMOSDTO customerHMOSDTO)
        {
            try
            {
                var customerHMOS = _mapper.Map<CustomerHMOS>(customerHMOSDTO);
                customerHMOS.IsActive = true;
                var newCustomer = await _context.CustomerHMOS.AddAsync(customerHMOS);
                await _context.SaveChangesAsync();
                return newCustomer.Entity.ID;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(CustomerHMOSDTO customerHMOSDTO)
        {
            try
            {
                var existingCustomerHMO = await _context.CustomerHMOS.FindAsync(customerHMOSDTO.ID);
                if (existingCustomerHMO == null)
                {
                    throw new Exception($"CustomerHMO with ID {customerHMOSDTO.ID} does not exist.");
                }

                _mapper.Map(customerHMOSDTO, existingCustomerHMO);
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
                var customerHMO = await _context.CustomerHMOS.FindAsync(id);
                if (customerHMO == null || !customerHMO.IsActive)
                {
                    throw new Exception($"CustomerHMO with ID {id} does not exist or is already inactive.");
                }

                customerHMO.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
