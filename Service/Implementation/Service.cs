using Domain.Domain_Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T CreateNew(T t)
        {
            return this._repository.Insert(t);
        }

        public T Delete(Guid id)
        {
            return this._repository.Delete(GetById(id));
        }

        public List<T> GetAll()
        {
            return this._repository.GetAll().ToList();
        }

        public T GetById(Guid? id)
        {
            return this._repository.Get(id);
        }

        public T Update(T t)
        {
            return this._repository.Update(t);
        }
    }
}
