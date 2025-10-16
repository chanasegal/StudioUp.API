using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IContactRepository
    {
        Task<List<ContactDTO>> GetAllAsync();
        Task<ContactDTO> GetByIdAsync(int id);
        Task<ContactDTO> AddAsync(ContactDTO contactDTO);
    }
}
