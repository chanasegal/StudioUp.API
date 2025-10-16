using System.Collections.Generic;
using System.Threading.Tasks;
using StudioUp.DTO;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerFixedTrainingRepository
    {
        Task<CustomerFixedTrainingDTO> AddAsync(CustomerFixedTrainingDTO entity);
        Task<List<CustomerFixedTrainingDTO>> GetAllByCustomerIdAsync(int customerId);
        Task DeleteAsync(int id);
    }
}
