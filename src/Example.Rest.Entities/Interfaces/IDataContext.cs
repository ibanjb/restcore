using System.Collections.Generic;
using Example.Rest.Entities.Models;

namespace Example.Rest.Entities.Interfaces
{
    public interface IDataContext
    {
        IList<Model> GetAll();

        Model GetAllById(int id);

        void Add(Model model);
        
        void Update(int id, Model model);

        void Delete(int id);
    }
}
