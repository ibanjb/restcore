using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Example.Rest.Entities.Interfaces;
using Example.Rest.Entities.Models;

namespace Example.Rest.Business
{
    public class BusinessContext : IBusinessContext
    {
        private readonly IDataContext _dataContext;

        public BusinessContext(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<IList<Model>> GetAllAsync()
        {
            try
            {
                IList<Model> result = _dataContext.GetAll();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromException<IList<Model>>(ex);
            }
        }

        public Task<Model> GetAllAsyncById(int id)
        {
            try
            {
                Model result = _dataContext.GetAllById(id);
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                return Task.FromException<Model>(ex);
            }
        }

        public Task AddAsync(Model model)
        {
            try
            {
                _dataContext.Add(model);
                return Task.FromResult(model);
            }
            catch (Exception ex)
            {
                return Task.FromException<Model>(ex);
            }
        }

        public Task UpdateAsync(int id, Model model)
        {
            try
            {
                _dataContext.Update(id, model);
                return Task.FromResult(model);
            }
            catch (Exception ex)
            {
                return Task.FromException<Model>(ex);
            }
        }

        public Task DeleteAsync(int id)
        {
            try
            {
                _dataContext.Delete(id);
                return Task.FromResult(id);
            }
            catch (Exception ex)
            {
                return Task.FromException<Model>(ex);
            }
        }
    }
}
