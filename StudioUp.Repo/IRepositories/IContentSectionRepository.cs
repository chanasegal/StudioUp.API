using StudioUp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentSectionRepository
    {
        Task<IEnumerable<ContentSectionDTO>> GetAllAsync();
        Task<ContentSectionDTO> GetByIdAsync(int id);
        Task<ContentSectionDTO> AddAsync(ContentSectionManagementDTO contentSection);
        Task UpdateAsync(ContentSectionManagementDTO contentSection);
        Task DeleteAsync(int ID);
        Task<IEnumerable<ContentSectionDTO>> GetByContentTypeAsync(int contentTypeId); // הוספת פונקציה חדשה
    }
}
