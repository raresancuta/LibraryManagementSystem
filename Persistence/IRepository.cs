using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IRepository<E>
    {
        E? FindById(int id);
        IEnumerable<E> FindAll();
        void Save(E entity);
        void Update(E entity);
        void Delete(E entity);
    }
}
