using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerTypeRepository
    {
        Task<List<CustomerTypeDTO>> GetAllAsync();
        Task<CustomerTypeDTO> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(CustomerTypeDTO customerType);
        Task<CustomerTypeDTO> AddAsync(CustomerTypeDTO customerType);
    }
}
