using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class LendingDBRepository : ILendingRepository
    {
        private readonly AppDbContext _context;
        public LendingDBRepository() 
        {
            _context = DBContextManager.GetContext();
        }
        public void Delete(Lending entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lending> FindAll()
        {   
            return _context.Lendings.ToList();
        }

        public Lending? FindOneActiveByBookAndClient(string title, string clientName)
        {
            return _context.Lendings.FirstOrDefault(l => l.Book.Title.Equals(title) && l.ClientName.Equals(clientName) && l.LendingStatus==LendingStatus.BookLent);
        }

        public IEnumerable<Lending> FindByBookTitle(string bookTitle)
        {
            
            var query = _context.Lendings.AsQueryable().Where(l=> l.Book.Title == bookTitle);
            return query.ToList();
        }

        public Lending? FindById(int id)
        {
            
            return _context.Lendings.FirstOrDefault(l=> l.Id == id);
        }

        public void Save(Lending entity)
        {
            
            _context.Lendings.Add(entity);
            _context.SaveChanges();
        }

        public void SetBookReturned(Lending lending)
        {
            
            if (lending != null)
            {
                if (lending.LendingStatus == LendingStatus.BookReturned)
                {
                    throw new LibraryException("Book already returned");
                }
                else
                {
                    lending.LendingStatus = LendingStatus.BookReturned;
                    _context.SaveChanges();
                }
            }
        }

        public void Update(Lending entity)
        {
            throw new NotImplementedException();
        }
    }
}
