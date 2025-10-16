using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;

namespace StudioUp.Repo.Repositories
{
    public class CustomerFixedTrainingRepository : ICustomerFixedTrainingRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly ILogger<CustomerFixedTrainingRepository> logger;

        public CustomerFixedTrainingRepository(DataContext context, IMapper mapper, ILogger<CustomerFixedTrainingRepository> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<CustomerFixedTrainingDTO> AddAsync(CustomerFixedTrainingDTO entity)
        {
            try
            {
                var newEntity = mapper.Map<CustomerFixedTraining>(entity);
                newEntity.IsActive = true;
                await context.CustomerFixedTrainings.AddAsync(newEntity);
                await context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while adding a new CustomerFixedTraining.");
                throw;
            }
        }

        public async Task<List<CustomerFixedTrainingDTO>> GetAllByCustomerIdAsync(int customerId)
        {
            try
            {
                var trainings = await context.CustomerFixedTrainings
                    .Where(t => t.CustomerId == customerId && t.IsActive)
                    .ToListAsync();
                return mapper.Map<List<CustomerFixedTrainingDTO>>(trainings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving CustomerFixedTrainings by customer ID.");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var training = await context.CustomerFixedTrainings.FindAsync(id);
                if (training == null || !training.IsActive)
                {
                    throw new Exception($"CustomerFixedTraining with ID {id} does not exist or is already inactive.");
                }

                training.IsActive = false;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting a CustomerFixedTraining.");
                throw;
            }
        }
    }
}
