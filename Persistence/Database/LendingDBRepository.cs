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

        public Lending? FindOneActiveByBookAndClient(Book book, string clientName)
        {
            return _context.Lendings.FirstOrDefault(l => l.Book.Title.Equals(book.Title) && l.ClientName.Equals(clientName) && l.LendingStatus == LendingStatus.Active);
        }

        public IEnumerable<Lending> FindActiveByBookTitle(Book book)
        {
            
            var query = _context.Lendings.AsQueryable().Where(l=> l.Book.Title == book.Title && l.LendingStatus == LendingStatus.Active);
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
                if (lending.LendingStatus == LendingStatus.Finished)
                {
                    throw new LibraryException("Book already returned");
                }
                else
                {
                    lending.LendingStatus = LendingStatus.Finished;
                    _context.SaveChanges();
                }
            }
        }

        public void Update(Lending entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lending> FindActiveByClient(string clientName)
        {
            return _context.Lendings.AsQueryable().Where(l => l.ClientName.Equals(clientName) && l.LendingStatus == LendingStatus.Active).ToList();
        }
    }
}
