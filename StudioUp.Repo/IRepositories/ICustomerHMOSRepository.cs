using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ICustomerHMOSRepository
    {
        public Task<IEnumerable<CustomerHMOSDTO>> GetAllAsync();
        public Task<CustomerHMOSDTO> GetByIdAsync(int id);
        public Task<int> AddAsync(CustomerHMOSDTO customerHMOSDTO);
        public Task UpdateAsync(CustomerHMOSDTO customerHMOSDTO);
        public Task DeleteAsync(int id);
    }
}
