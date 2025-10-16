using StudioUp.DTO;
using StudioUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface ILeumitCommimentTypesRepository
    {
        Task<List<LeumitCommimentTypesDTO>> GetAllAsync();
        Task<LeumitCommimentTypesDTO> GetByIdAsync(int id);
        Task<LeumitCommimentTypesDTO> UpdateAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO);
        Task AddAsync(LeumitCommimentTypesDTO leumitCommimentTypesDTO);
        Task<bool> DeleteAsync(int id);
    }
}
