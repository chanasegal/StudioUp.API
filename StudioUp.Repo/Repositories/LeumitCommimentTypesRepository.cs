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
    public class LeumitCommimentTypesRepository : ILeumitCommimentTypesRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public LeumitCommimentTypesRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task AddAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO)
        {
            try
            {
                var entity = mapper.Map<LeumitCommimentTypes>(leumitCommimentTypesDTO);
                entity.IsActive = true; 
                context.LeumitCommimentTypes.Add(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception("Error adding LeumitCommitmentType", exception);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await context.LeumitCommimentTypes.FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
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
                throw new Exception("Error deleting LeumitCommitmentType", exception);
            }
        }



        public async Task<List<LeumitCommimentTypesDTO>> GetAllAsync()
        {
            try
            {
                var activeEntities = await context.LeumitCommimentTypes
                                                  .Where(l => l.IsActive)
                                                  .ToListAsync();
                return mapper.Map<List<LeumitCommimentTypesDTO>>(activeEntities);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving all LeumitCommitmentTypes", exception);
            }
        }

        public async Task<LeumitCommimentTypesDTO> GetByIdAsync(int id)
        {
            try
            {
                var entity = await context.LeumitCommimentTypes
                                          .FirstOrDefaultAsync(l => l.Id == id && l.IsActive);
                if (entity == null)
                {
                    return null;
                }
                return mapper.Map<LeumitCommimentTypesDTO>(entity);
            }
            catch (Exception exception)
            {
                throw new Exception("Error retrieving LeumitCommitmentType by ID", exception);
            }
        }

        public async Task<LeumitCommimentTypesDTO> UpdateAsync(LeumitCommimentTypesDTO lc)
        {
            try
            {
                var existingEntity = await context.LeumitCommimentTypes
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
                throw new Exception("Error updating LeumitCommitmentType", exception);
            }
        }
    }
}
