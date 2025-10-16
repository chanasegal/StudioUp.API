using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingRepository
    {
        public Task<List<TrainingDTO>> GetAllTrainings();
        public Task<List<CalanderTrainingDTO>> GetAllTrainingsCalender();
        public Task<TrainingDTO> GetTrainingById(int id);
        public Task<TrainingPostDTO> AddTraining(TrainingPostDTO trainingDto);
        public Task UpdateTraining(TrainingDTO trainingDto);
        public Task DeleteTraining(int id);
        public Task<List<CalanderTrainingDTO>> GetByCustomerTypeForCalander(int customerTypeId);

    }
}
