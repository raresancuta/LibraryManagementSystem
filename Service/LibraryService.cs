using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Validators;
using MachineLearning;
namespace Service
{
    public class LibraryService : IService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILendingRepository _lendingRepository;
        private readonly Validator<Book> _bookValidator;
        private readonly Validator<Lending> _lendingValidator;

        public LibraryService(IBookRepository bookRepository, ILendingRepository lendingRepository)
        {
            _bookRepository = bookRepository;
            _lendingRepository = lendingRepository;
            _bookValidator = new BookValidator();
            _lendingValidator = new LendingValidator();
        }

        public void AddBook(Book book)
        {
            
            var b = _bookRepository.FindByTitle(book.Title);
            if (b == null)
            {
             
                _bookValidator.Validate(book);
                _bookRepository.Save(book);
            }
            else
            {
                _bookRepository.Update(book);
            }
        }

        public void AddBookCopies(Book book, int noOfCopies)
        {
            var b = _bookRepository.FindByTitle(book.Title);
            if (b != null)
            {
                _bookRepository.AddCopy(b, noOfCopies);
                _bookRepository.UpdateQuantity(b, noOfCopies);
            }
            else throw new LibraryException("Book not found");
        }

        public void DeleteBook(Book book)
        {
            var b = _bookRepository.FindByTitle(book.Title);
            if (b != null)
            {
                _bookRepository.Delete(b);
            }
            else throw new LibraryException("There is no book with this title");
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.FindAll();
        }

        public void RemoveBookCopies(Book book, int noOfCopies)
        {
            var b = _bookRepository.FindByTitle(book.Title);
            if (b != null)
            {
                if (noOfCopies > b.NoOfAvailableCopies)
                {
                    _bookRepository.UpdateQuantity(b, -noOfCopies);
                    _bookRepository.RemoveCopy(b, noOfCopies);
                }
            }
            else throw new LibraryException("Book not found");
        }

        public void LendBook(Lending lending)
        {
            var b = _bookRepository.FindByTitle(lending.Book.Title);
            if (b != null)
            {
                lending.Book = b;
                _lendingValidator.Validate(lending);
                _lendingRepository.Save(lending);
                _bookRepository.RemoveCopy(lending.Book, 1);
            }
            else throw new LibraryException("Book not found");
        }

        public void ReturnBook(Lending lending)
        {
            var b = _bookRepository.FindByTitle(lending.Book.Title);
            if (b != null)
            {
                var l = _lendingRepository.FindOneActiveByBookAndClient(b.Title,lending.ClientName);
                if (l != null)
                {
                    _lendingRepository.SetBookReturned(l);
                    _bookRepository.AddCopy(l.Book, 1);   
                }
                else throw new LibraryException("Lending not found");
            }
            else throw new LibraryException("Book not found");
            
        }

        public IEnumerable<Book> SearchBooks(Book book)
        {
            return _bookRepository.GetFilteredBooks(book);
        }

        public void UpdateBook(Book book)
        {
            var old_book = _bookRepository.FindByTitle(book.Title);
            if (old_book != null)
            {
                book.Id = old_book.Id;
                _bookValidator.Validate(book);
                _bookRepository.Update(old_book,book);
            }
            else throw new LibraryException("There is no book with this title");
        }
        
        public IEnumerable<Book> GetMostLentBooks(int topN)
        {
            return RecommendationService.GetMostLentBooks(topN,_bookRepository.FindAll(),_lendingRepository.FindAll());   
        }

       
        public IEnumerable<Book> GetRecommendationsByAuthorPopularity()
        {
            return RecommendationService.GetRecommendationsByAuthorPopularity(_bookRepository.FindAll(), _lendingRepository.FindAll());
        }

        
        public IEnumerable<Book> GetSeasonalRecommendations()
        {
            return RecommendationService.GetSeasonalRecommendations(_bookRepository.FindAll(), _lendingRepository.FindAll());
        }
        public float GetPreditionOfLendingABook(Book book)
        {
            var b = _bookRepository.FindByTitle(book.Title);
            if (b != null)
            {
                return RecommendationService.GetPreditionOfLendingABook(b, _lendingRepository.FindAll());   
            }
            else throw new LibraryException("Book not found");
        }
    }
}
