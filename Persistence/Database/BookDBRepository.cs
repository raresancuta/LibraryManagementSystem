using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class BookDBRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookDBRepository() 
        {
            _context = DBContextManager.GetContext();
        }
        public void AddCopy(Book entity, int noOfCopies)
        {
           
            var book = FindByTitle(entity.Title);
            if(book != null)
            {
                book.NoOfAvailableCopies += noOfCopies;
                _context.SaveChanges();    
            }
        }

        public void UpdateQuantity(Book entity,int noOfCopies)
        {
            
            if (entity != null)
            {
                if (noOfCopies < 0 && -(noOfCopies) > entity.NoOfAvailableCopies)
                {
                    throw new LibraryException("There are not enough books");
                }
                else
                {
                    entity.Quantity += noOfCopies;
                    _context.SaveChanges();
                }
            }
        }

        public void Delete(Book entity)
        {
            var book = FindByTitle(entity.Title);
            if (book != null)
            {
                if (book.NoOfAvailableCopies < book.Quantity)
                {
                    throw new LibraryException("Cannot delete the book. There are active lendings registered!");
                }
                else
                {
                    _context.Remove(book);
                    _context.SaveChanges();
                }
            }
        }

        public void RemoveCopy(Book entity, int noOfCopies)
        {
            if (entity != null)
            {
                if (noOfCopies > entity.NoOfAvailableCopies)
                {
                    throw new LibraryException("There are not enough books");
                }
                else
                {
                    entity.NoOfAvailableCopies -= noOfCopies;
                    _context.SaveChanges();
                }
            }
        }

        public IEnumerable<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book? FindById(int id)
        {
            return _context.Books.FirstOrDefault(book => book.Id == id);
        }

        public Book? FindByTitle(string title)
        {
            return _context.Books.FirstOrDefault(book=> book.Title.Equals(title));
        }

        public IEnumerable<Book> GetFilteredBooks(Book book)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(book.Title))
                query = query.Where(b => b.Title.Contains(book.Title));

            if (!string.IsNullOrEmpty(book.Author))
                query = query.Where(b => b.Author.Contains(book.Author));

            if (!string.IsNullOrEmpty(book.ISBN))
                query = query.Where(b => b.ISBN == book.ISBN);

            return query.ToList();
        }


        public void Save(Book entity)
        {
            _context.Books.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Book entity)
        {
            if (entity != null)
            {
                _context.Books.Update(entity);
                _context.SaveChanges();
            }
        }

        public void Update(Book old_book, Book new_book)
        {
            if(new_book != null)
            {
                new_book.Id = old_book.Id;
                Delete(old_book);
                Save(new_book);
                _context.SaveChanges();
            }
        }
    }
}
