using AutoMapper;
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
    public class PaymentOptionRepository : IRepository<PaymentOptionDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public PaymentOptionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentOptionDTO>> GetAllAsync()
        {
            try
            {
                var x = await _context.PaymentOptions.Where(y => y.IsActive).ToListAsync();
                return _mapper.Map<List<PaymentOptionDTO>>(x);
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaymentOptionDTO> GetByIdAsync(int id)
        {
            try
            {
                 var x=_mapper.Map<PaymentOptionDTO>(await _context.PaymentOptions.FindAsync(id));
                if (x.IsActive)
                    return x;
                else
                    throw new Exception($"cant find payment option by ID {id}");
            }
            catch 
            {
                throw;
            }
        }

        public async Task<PaymentOptionDTO> AddAsync(PaymentOptionDTO paymentOption)
        {
            try
            {
                var p = await _context.PaymentOptions.AddAsync(_mapper.Map<PaymentOption>(paymentOption));
                await _context.SaveChangesAsync();
                return paymentOption;
            }
            catch 
            {
                throw;
            }

        }

        public async Task UpdateAsync(PaymentOptionDTO paymentOption)
        {
            try
            {
                _context.PaymentOptions.Update(_mapper.Map<PaymentOption>(paymentOption));
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
                var paymentOption = await _context.PaymentOptions.FindAsync(id);
                if (paymentOption == null||paymentOption.IsActive==false)
                {
                    throw new Exception($"cant find payment option by ID {id}");
                }
                paymentOption.IsActive = false;
                    await _context.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }

        }

       
    }
}
