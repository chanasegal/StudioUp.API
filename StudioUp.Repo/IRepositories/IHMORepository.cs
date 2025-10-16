using StudioUp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IHMORepository
    {
        Task<List<HMODTO>> GetAllAsync();
        Task<HMODTO> GetByIdAsync(int id);

        Task<bool> UpdateAsync( HMODTO hmo);


        Task<HMODTO> AddAsync(HMODTO hmo);

        Task DeleteAsync(int id);
    }
}
