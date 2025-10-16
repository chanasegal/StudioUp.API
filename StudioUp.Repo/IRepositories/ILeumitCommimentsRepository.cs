using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ILeumitCommimentsRepository
    {
        Task<List<LeumitCommitmentsDTO>> GetAllAsync();
        Task<LeumitCommitmentsDTO> GetByIdAsync(string id);
        Task<LeumitCommitmentsDTO> UpdateAsync(LeumitCommitmentsDTO leumitCommitmentsDTO);
        Task AddAsync(LeumitCommitmentsDTO leumitCommitmentsDTO);
        Task<bool> DeleteAsync(string id);

    }
}
