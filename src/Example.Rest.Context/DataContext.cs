using System.Collections.Generic;
using Example.Rest.Entities.Interfaces;
using Example.Rest.Entities.Models;

namespace Example.Rest.Context
{
    public class DataContext : IDataContext
    {
        public DataContext()
        {
            // put your instances here
        }

        public IList<Model> GetAll()
        {
            // put your code here

            // method mocked
            List<Model> result = new List<Model>();
            result.Add(GetMock(1));
            result.Add(GetMock(2));
            result.Add(GetMock(3));
            return result;
        }

        public Model GetAllById(int id)
        {
            // put your code here

            // method mocked
            Model result = new Model();
            result = GetMock(id);
            return result;
        }

        public void Add(Model model)
        {
            // put your code here
        }

        public void Update(int id, Model model)
        {
            // put your code here
        }

        public void Delete(int id)
        {
            // put your code here
        }

        /// <summary>
        /// Remove it !!!
        /// </summary>
        /// <param name="id">Mock data identifier</param>
        /// <returns></returns>
        private Model GetMock(int id)
        {
            return new Model() { Id = id, Description = "Description for " + id};
        }
    }
}
