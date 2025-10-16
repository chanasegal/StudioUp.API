using StudioUp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IContentTypeRepository
    {
        Task<IEnumerable<ContentTypeDTO>> GetAll();
        Task<ContentTypeDTO> GetById(int id);
        Task<ContentTypeDTO> GetByIdWithContentSection(int id);
        Task<ContentTypeDTO> GetByIdWithContentSectionHPOnly(int id);
        Task<ContentTypeDTO> Create(ContentTypeDTO contentType);
        Task Update(ContentTypeDTO contentType);
        Task Delete(int id);
    }
}
