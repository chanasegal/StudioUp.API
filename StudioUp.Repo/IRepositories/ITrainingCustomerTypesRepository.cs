using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingCustomerTypesRepository
    {
       Task<List<TrainingCustomerTypeDTO>> GetAllAsync();
        Task<TrainingCustomerTypeDTO> GetByIdAsync(int id);
        Task<TrainingCustomerTypePostComand> AddAsync(TrainingCustomerTypePostComand trainingCustomerTypedto);
        Task UpdateAsync(TrainingCustomerTypePostComand trainingCustomerTypedto);
        Task DeleteAsync(int id);
        Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes();

    }
}
