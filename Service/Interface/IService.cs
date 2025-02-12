using Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IService<T> where T : BaseEntity
    {
        public List<T> GetAll();
        public T GetById(Guid? id);
        public T CreateNew(T t);
        public T Update(T t);
        public T Delete(Guid id);
    }
}
