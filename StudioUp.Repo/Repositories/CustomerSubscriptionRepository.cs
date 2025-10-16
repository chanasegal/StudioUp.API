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
    public class CustomerSubscriptionRepository : ICustomerSubscriptionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public CustomerSubscriptionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerSubscription>> GetAllCustomerSubscriptionsAsync()
        {
            try
            {
                return await _context.CustomerSubscriptions
                    .Include(c => c.Customer)
                    .Include(s => s.SubscriptionType)
                    .Where(s => s.IsActive)
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CustomerSubscriptionDTO>> GetCustomerSubscriptionsByCustomerIdAsync(int customerId)
        {
            try
            {
                var subscriptions = await _context.CustomerSubscriptions
                    .Where(cs => cs.CustomerID == customerId && cs.IsActive)
                    .ToListAsync();

                return _mapper.Map<List<CustomerSubscriptionDTO>>(subscriptions);

            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerSubscriptionDTO> GetCustomerSubscriptionByIdAsync(int id)
        {
            try
            {
                var subscription = await _context.CustomerSubscriptions.FirstOrDefaultAsync(t => t.ID == id && t.IsActive);
                if (subscription == null)
                {
                    throw new Exception($"CustomerSubscription with ID {id} does not exist or is inactive.");
                }
                var mapSub = _mapper.Map<CustomerSubscriptionDTO>(subscription);
                return mapSub;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerSubscriptionDTO> AddCustomerSubscriptionAsync(CustomerSubscriptionDTO subscription)
        {
            try
            {
                var mapCastS = _mapper.Map<CustomerSubscription>(subscription);
                await _context.CustomerSubscriptions.AddAsync(mapCastS);
                await _context.SaveChangesAsync();
                return subscription;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCustomerSubscriptionAsync(CustomerSubscriptionDTO subscription)
        {
            try
            {
                var existingSubscription = await _context.CustomerSubscriptions.FindAsync(subscription.ID);

                if (existingSubscription == null)
                {
                    throw new Exception($"CustomerSubscription with ID {subscription.ID} does not exist.");
                }
                _mapper.Map(subscription, existingSubscription);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCustomerSubscriptionAsync(int id)
        {
            try
            {
                var customerSubscription = await _context.CustomerSubscriptions.FindAsync(id);

                if (customerSubscription == null || !customerSubscription.IsActive)
                {
                    throw new Exception($"CustomerSubscription with ID {id} does not exist or is already inactive.");
                }

                customerSubscription.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
