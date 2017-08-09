using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Rest.Entities.Models;

namespace Example.Rest.Entities.Interfaces
{
    public interface IBusinessContext
    {
        Task<IList<Model>> GetAllAsync();

        Task<Model> GetAllAsyncById(int id);

        Task AddAsync(Model model);

        Task UpdateAsync(int id, Model model);

        Task DeleteAsync(int id);
    }
}
