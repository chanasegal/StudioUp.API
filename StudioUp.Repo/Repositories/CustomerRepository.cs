using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using MailKit.Security;

namespace StudioUp.Repo.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(DataContext context, IMapper mapper, ILogger<CustomerRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDTO> AddAsync(CustomerDTO entity)
        {
            try
            {
                var mapCast = mapper.Map<Customer>(entity);
                var newCustomer = await context.Customers.AddAsync(mapCast);
                var x = await context.Customers.AddAsync(mapper.Map<Customer>(entity));

          /*    var mapCast = mapper.Map<Customer>(entity);
                var newCustomer = await context.Customers.AddAsync(mapCast);*/

                await context.SaveChangesAsync();

                return entity;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerDTO> GetCustomerByEmailAndPassword(string email, string password)
        {
            try
            {
                var login = await context.Login.FirstOrDefaultAsync(l => l.Email == email && l.Password == password);
                if (login is not null)
                {
                    try
                    {
                        var cust = await context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.IsActive);
                        var mapCust = mapper.Map<CustomerDTO>(cust);
                        return mapCust;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An error occurred while retrieving the customer by email and password.", ex);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerDTO> GetCustomerByEmail(string email)
        {
            try
            {
                var cust = await context.Customers.FirstOrDefaultAsync(c => c.Email == email && c.IsActive);
                var mapCust = mapper.Map<CustomerDTO>(cust);
                return mapCust;

            }
            catch
            {
                throw;
            }
        }

        public async Task<string> GetPasswordrByEmail(string email)
        {
            try
            {
                var login = await context.Login.FirstOrDefaultAsync(c => c.Email == email);
                if (login == null) 
                {
                    return null;
                }
                var password = login.Password;
                return password;
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
                var customers = await context.Customers.FindAsync(id);
                if (customers == null || !customers.IsActive)
                {
                    throw new Exception($"Customer with ID {id} does not exist or is already inactive.");
                }

                customers.IsActive = false;
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CustomerDTO>> GetAllAsync()
        {
            try
            {
                var customers = await context.Customers.Where(ct => ct.IsActive).ToListAsync();
                return mapper.Map<List<CustomerDTO>>(customers);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            try
            {
                var customer = await context.Customers.FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
                if (customer == null)
                {
                    throw new Exception($"Customer with ID {id} does not exist or is inactive.");
                }
                var mapCust = mapper.Map<CustomerDTO>(customer);
                return mapCust;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(CustomerDTO entity)
        {
            try
            {
                var existingCustomer = await context.Customers.FindAsync(entity.Id);
                if (existingCustomer == null)
                {
                    throw new Exception($"Customer with ID {entity.Id} does not exist.");
                }

                mapper.Map(entity, existingCustomer);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CustomerDTO>> FilterAsync(CustomerFilterDTO filter)
        {
            try
            {
                var query = context.Customers.AsQueryable();

                if (!string.IsNullOrEmpty(filter.FirstName) || !string.IsNullOrEmpty(filter.LastName))
                {
                    var firstName = filter.FirstName?.Trim().Replace(" ", "").ToLower();
                    var lastName = filter.LastName?.Trim().Replace(" ", "").ToLower();

                    query = query.Where(c =>
                        (string.IsNullOrEmpty(firstName) || c.FirstName.ToLower().Replace(" ", "").Contains(firstName)) &&
                        (string.IsNullOrEmpty(lastName) || c.LastName.ToLower().Replace(" ", "").Contains(lastName)));
                }

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    var email = filter.Email.Trim().Replace(" ", "").ToLower();
                    query = query.Where(c => c.Email.ToLower().Replace(" ", "").Contains(email));
                }

                return await query.Select(c => new CustomerDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Email = c.Email,
                    Tel = c.Tel,
                    PaymentOptionId = c.PaymentOptionId.Value,
                    HMOId = c.HMOId.Value,
                    CustomerTypeId = c.CustomerTypeId.Value,
                    SubscriptionTypeId = c.SubscriptionTypeId.Value,
                    IsActive = c.IsActive
                }).Where(ct => ct.IsActive).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Up Studio", "UpStudio@example.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.example.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("UpStudio@example.com", "password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
