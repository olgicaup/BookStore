using Domain.Identity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<IntegratedSystemsUser> GetAll();
        IntegratedSystemsUser Get(string? id);
        void Insert(IntegratedSystemsUser entity);
        void Update(IntegratedSystemsUser entity);
        void Delete(IntegratedSystemsUser entity);
    }
}
