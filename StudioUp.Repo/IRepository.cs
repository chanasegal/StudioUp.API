using StudioUp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioUp.Repo
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(int id);
/*        Task AddAsync(Func<object, object> map, TrainingTypeDTO trainingTypeDto);
*/    }
}
