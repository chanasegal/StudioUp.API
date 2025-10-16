using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StudioUp.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
namespace StudioUp.Repo.Repositories
{
    public class TrainingTypeRepository : IRepository<TrainingTypeDTO>
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrainingTypeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingTypeDTO>> GetAllAsync()
        {
            try
            {
                var trainingTypes = await _context.TrainingTypes.Where(t => t.IsActive).ToListAsync();
                return _mapper.Map<List<TrainingTypeDTO>>(trainingTypes);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Types List.", ex);
            }

        }

        public async Task<TrainingTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                var trainingType = await _context.TrainingTypes.Where(t => t.ID == id && t.IsActive).FirstOrDefaultAsync();
                return _mapper.Map<TrainingTypeDTO>(trainingType);
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training Type.", ex);
            }

        }

        public async Task<TrainingTypeDTO> AddAsync(TrainingTypeDTO trainingTypeDto)
        {
            try
            {
                if (trainingTypeDto == null)
                    throw new Exception("Training cannot be null");
                var trainingType = _mapper.Map<TrainingType>(trainingTypeDto);
                trainingType.IsActive = true;
                var newTrainingType = await _context.TrainingTypes.AddAsync(trainingType);
                await _context.SaveChangesAsync();
                trainingTypeDto.ID = newTrainingType.Entity.ID;
                return trainingTypeDto;
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add the Training Type.", ex);
            }
        }

        public async Task UpdateAsync(TrainingTypeDTO trainingTypeDto)
        {
            try
            {
                var trainingType = await _context.TrainingTypes.FindAsync(trainingTypeDto.ID);
                if (trainingType == null)
                {
                    throw new Exception("Training Type not found.");
                }
                _mapper.Map(trainingTypeDto, trainingType);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to update the Training Type.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var trainingType = await _context.TrainingTypes.FindAsync(id);
                if (trainingType == null || !trainingType.IsActive)
                {
                    return;
                }
                trainingType.IsActive = false;
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the Training Type.", ex);
            }
        }
    }
}

