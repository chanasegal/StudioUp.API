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
    public class LeumitCommimentRepository : ILeumitCommimentsRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public LeumitCommimentRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task AddAsync(LeumitCommitmentsDTO leumitCommitmentsDTO)
        {
            try
            {
                var entity = mapper.Map<LeumitCommitments>(leumitCommitmentsDTO);
                entity.IsActive = true;
                context.LeumitCommitments.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception("Error adding LeumitCommitment", exception);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var entity = await context.LeumitCommitments.FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
                if (entity == null)
                {
                    return false;
                }

                entity.IsActive = false;
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Error deleting LeumitCommitment", exception);
            }
        }

        public async Task<List<LeumitCommitmentsDTO>> GetAllAsync()
        {
            try
            {
                var activeEntities = await context.LeumitCommitments
                                                  .Where(l => l.IsActive)
                                                  .ToListAsync();
                return mapper.Map<List<LeumitCommitmentsDTO>>(activeEntities);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving all LeumitCommitments", exception);
            }
        }

        public async Task<LeumitCommitmentsDTO> GetByIdAsync(string id)
        {
            try
            {
                var entity = await context.LeumitCommitments
                                          .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
                if (entity == null)
                {
                    return null;
                }
                return mapper.Map<LeumitCommitmentsDTO>(entity);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving LeumitCommitment by ID", exception);
            }
        }

        public async Task<LeumitCommitmentsDTO> UpdateAsync(LeumitCommitmentsDTO lc)
        {
            try
            {
                var existingEntity = await context.LeumitCommitments
                                                  .FirstOrDefaultAsync(l => l.Id == lc.Id && l.IsActive);
                if (existingEntity == null)
                {
                    return null;
                }

                mapper.Map(lc, existingEntity);
                await context.SaveChangesAsync();
                return lc;
            }
            catch (Exception exception)
            {
                throw new Exception("Error updating LeumitCommitment", exception);
            }
        }
    }
}
