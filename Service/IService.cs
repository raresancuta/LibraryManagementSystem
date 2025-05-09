using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IService
    {
        IEnumerable<Book> GetAllBooks();
        IEnumerable<Book> SearchBooks(Book book);
        void AddBook(Book book);
        void DeleteBook(Book book);
        void UpdateBook(Book book);
        void AddBookCopies(Book book,int noOfCopies);
        void RemoveBookCopies(Book book,int noOfCopies);
        void LendBook(Lending lending);
        void ReturnBook(Lending lending);
        float GetPreditionOfLendingABook(Book book);
        IEnumerable<Book> GetMostLentBooks(int topN);
        IEnumerable<Book> GetRecommendationsByAuthorPopularity();
        IEnumerable<Book> GetSeasonalRecommendations();
    }
}
