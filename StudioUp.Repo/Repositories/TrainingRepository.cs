using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudioUp.DTO;
using StudioUp.Models;
using StudioUp.Repo.IRepositories;
using StudioUp.Repo.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;


        public TrainingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TrainingDTO>> GetAllTrainings()
        {
            try
            {
                var lst = await _context.Trainings.Where(t => t.IsActive)
                              .Include(t => t.TrainingCustomerType.CustomerType)
                              .Include(t => t.TrainingCustomerType.TrainingType)
                              .Include(t => t.Trainer)
                              .ToListAsync();
                return _mapper.Map<List<TrainingDTO>>(lst);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Trainings List.", ex);
            }

        }


        public async Task<List<CalanderTrainingDTO>> GetAllTrainingsCalender()
        {
            try
            {
                List<Training> lst = await _context.Trainings.Where(t => t.IsActive)
                 .Include(t => t.TrainingCustomerType.CustomerType)
                 .Include(t => t.TrainingCustomerType.TrainingType)
                 .Include(t => t.Trainer)
                 .ToListAsync();
                return _mapper.Map<List<CalanderTrainingDTO>>(lst);
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Trainings Calender List.", ex);
            }
        }

        public async Task<TrainingDTO> GetTrainingById(int id)
        {
            try
            {

                Training training = await _context.Trainings.Where(t => t.ID == id && t.IsActive)
                 .Include(t => t.TrainingCustomerType.CustomerType)
                 .Include(t => t.TrainingCustomerType.TrainingType)
                 .Include(t => t.Trainer)
                 .FirstOrDefaultAsync();
                return _mapper.Map<TrainingDTO>(training);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to get the Training", ex);
            }

        }

        public async Task<TrainingPostDTO> AddTraining(TrainingPostDTO trainingDto)
        {
            try
            {
                if (trainingDto == null)
                {
                    throw new Exception("Training cannot be null");
                }
                Training training = _mapper.Map<Training>(trainingDto);
                training.IsActive = true;
                var newtraining = await _context.Trainings.AddAsync(training);
                await _context.SaveChangesAsync();
                training.ID = newtraining.Entity.ID;
                return trainingDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to add the Training.", ex);
            }
        }

        public async Task UpdateTraining(TrainingDTO trainingDto)
        {
            try
            {
                Training training = await _context.Trainings.FindAsync(trainingDto.ID);
                if (training == null)
                    throw new Exception("Training not found.");
                _mapper.Map(trainingDto, training);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to update the Training.", ex);
            }

        }

        public async Task DeleteTraining(int id)
        {
            try
            {
                var training = await _context.Trainings.FindAsync(id);
                if (training == null || !training.IsActive)
                {
                    return;
                }
                training.IsActive = false;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while attempting to delete the Training.", ex);
            }

        }

        public async Task<List<CalanderTrainingDTO>> GetByCustomerTypeForCalander(int customerTypeId)
        {
            var all = await _context.TrainingCustomersTypes.Where(x => x.CustomerTypeID == customerTypeId).Select(t => t.Id).ToListAsync();

            List<Training> lst = await _context.Trainings.Where(y => all.Contains((int)(y.TrainingCustomerTypeId)))
                            .Include(t => t.TrainingCustomerType.CustomerType)
                            .Include(t => t.TrainingCustomerType.TrainingType)
                            .Include(t => t.Trainer)
                            .ToListAsync();
            return _mapper.Map<List<CalanderTrainingDTO>>(lst);
        }

        
    }
}
