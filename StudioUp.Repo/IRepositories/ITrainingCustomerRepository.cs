using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingCustomerRepository
    {
        Task<List<TrainingCustomerDTO>> GetAllTrainingCustomers();
        Task<TrainingCustomerDTO> GetTrainingCustomerById(int id);
        Task<List<TrainingCustomerDTO>> GetTrainingCustomerByCustomerId(int id);
        Task<List<CalanderAvailableTrainingDTO>> GetAllRegisteredTrainingsDetailsAsync();

        Task<List<TrainingCustomerDTO>> GetTrainingCustomerByTrainingId(int id);
        Task<TrainingCustomerDTO> AddTrainingCustomer(TrainingCustomerDTO trainingCustomer);
        Task UpdateTrainingCustomer(TrainingCustomerDTO trainingCustomer);       
        Task DeleteTrainingCustomer(int id);
        Task<List<CalanderAvailableTrainingDTO>> FilterAsync(CalanderAvailableTrainingFilterDTO filter);
        Task AddTrainingForCustomer(int TrainingId, int CustomerId);
    }
}
