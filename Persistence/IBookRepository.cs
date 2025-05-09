using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IBookRepository:IRepository<Book>
    {
        Book? FindByTitle(string title);
        void AddCopy(Book entity, int quantity);
        void RemoveCopy(Book entity,int quantity);
        IEnumerable<Book> GetFilteredBooks(Book book);
        void Update(Book old_book,Book new_book);
        void UpdateQuantity(Book entity, int noOfCopies);
    }
}
