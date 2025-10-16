using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudioUp.DTO;
using AutoMapper;
namespace StudioUp.Repo.Repositories
{
    public class TrainigTypeRepository : IRepository<TrainingTypeDTO>
    {


        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TrainigTypeRepository(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task<List<TrainingTypeDTO>> GetAllAsync()
        {
            return _mapper.Map<List<TrainingTypeDTO>> (await _context.TrainingTypes.ToListAsync());
        }

        public async Task<TrainingTypeDTO> GetByIdAsync(int id)
        {
            try
            {
                return _mapper.Map <TrainingTypeDTO>( await _context.TrainingTypes.FindAsync(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<TrainingTypeDTO> AddAsync(TrainingTypeDTO TrainingType)
        {
            try
            {
                var t = await _context.TrainingTypes.AddAsync(_mapper.Map<TrainingType>( TrainingType));
            await _context.SaveChangesAsync();
            return TrainingType;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(TrainingTypeDTO TrainingType)
        {
            try
            {

                _context.TrainingTypes.Update(_mapper.Map<TrainingType>(TrainingType));
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
                var TrainingType = await _context.TrainingTypes.FindAsync(id);
            if (TrainingType == null)
                {
                    throw new Exception($"cant find TrainingTypes by ID {id}");
                }
                _context.TrainingTypes.Remove(TrainingType);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        
    }
}

