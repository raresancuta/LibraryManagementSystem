using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface ILendingRepository:IRepository<Lending>
    {
        IEnumerable<Lending> FindActiveByBookTitle(Book book);
        void SetBookReturned(Lending lending);
        Lending? FindOneActiveByBookAndClient(Book book, string clientName);
        IEnumerable<Lending> FindActiveByClient(string clientName);

    }
}
