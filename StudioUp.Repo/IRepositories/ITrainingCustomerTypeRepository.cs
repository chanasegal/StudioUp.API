using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ITrainingCustomerTypeRepository
    {
        Task<List<TrainingCustomerTypeDTO>> GetAllTrainingCustomerTypes();
        //Task<List<DTO.TrainingCustomerTypeDTO>> GetActiveTrainingCustomerTypes();

        Task<TrainingCustomerTypeDTO> GetTrainingCustomerTypeById(int id);
        Task<TrainingCustomerTypePostComand> UpdateTrainingCustomerType(int id, DTO.TrainingCustomerTypePostComand trainingCustomerTypedto);
        Task<DTO.TrainingCustomerTypePostComand> AddTrainingCustomerType(DTO.TrainingCustomerTypePostComand TrainingCustomerType);
        Task<TrainingCustomerTypeDTO> DeleteTrainingCustomerType(int id);
    }
}
