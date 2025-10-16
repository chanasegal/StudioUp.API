using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IAvailableTrainingRepository
    {
        Task<IEnumerable<AvailableTrainingDTO>> GetAllAvailableTrainingsAsync();
        Task<IEnumerable<CalanderAvailableTrainingDTO>> GetAllAvailableTrainingsAsyncForCalander();
        Task<AvailableTrainingDTO> GetAvailableTrainingByIdAsync(int id);
        Task<CalanderAvailableTrainingDTO> GetAvailableByTrainingIdForCalander(int id);

        Task<AvailableTrainingDTO> GetAvailableTrainingByTrainingIdAsync(int id);
        Task<List<CalanderAvailableTrainingDTO>> GetAllTrainingsDetailsForCustomerAsync(int customerId);
        Task<AvailableTrainingDTO> AddAvailableTrainingAsync(AvailableTrainingDTO availableTrainingDTO);
        Task UpdateAvailableTrainingAsync( AvailableTrainingDTO availableTrainingDTO);
        Task DeleteAvailableTrainingAsync(int id);
       public Task<bool> GenerateAvailableTrainings(DateOnly startDate, DateOnly? endDate, bool isWeekEnd);

    }
}
