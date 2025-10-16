using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainerRepository
    {
        Task<List<TrainerDTO>> GetAllTrainersAsync();
        Task<TrainerDTO> GetTrainerByIdAsync(int id);
        Task UpdateTrainerAsync(TrainerDTO trainer);
        Task<TrainerDTO> AddTrainerAsync(TrainerDTO trainer);
        Task DeleteTrainerAsync(int id);
        Task<List<TrainerDTO>> FilterTrainerAsync(TrainerFilterDto filter);
    }
}
