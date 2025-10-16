using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo.IRepositories
{
    public interface IInternalHomeLinksRepository
    {
        Task<List<InternalHomeLinksDTO>> GetAllLinks();
        Task<InternalHomeLinksDTO> GetLinkById(int id);
        Task Update( InternalHomeLinksDTO link);
        Task<InternalHomeLinksDTO> AddAsync(InternalHomeLinksDTO link);
        Task Delete(int id);
    }
}
