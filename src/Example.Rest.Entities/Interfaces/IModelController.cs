using System.Threading.Tasks;
using Example.Rest.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Rest.Entities.Interfaces
{
    public interface IModelController
    {
        Task<IActionResult> GetById(int id);

        Task<IActionResult> GetAll();

        Task<IActionResult> Create([FromBody]Model model);

        Task<IActionResult> Update(int id, [FromBody] Model model);

        Task<IActionResult> Delete(int id);
    }
}
