using MachineLearning;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RecommendationService
    {
        public static IEnumerable<Book> GetMostLentBooks(int topN,IEnumerable<Book> books, IEnumerable<Lending> lendings)
        {
            return lendings
                .Where(l => l.Book != null)
                .GroupBy(l => l.Book.Id)
                .OrderByDescending(g => g.Count())
                .Take(topN)
                .Select(g => books.FirstOrDefault(b => b.Id == g.Key))
                .Where(b => b != null)
                .ToList();
        }

        
        public static IEnumerable<Book> GetRecommendationsByAuthorPopularity(IEnumerable<Book> books, IEnumerable<Lending> lendings)
        {
            var popularAuthors = lendings
                .Where(l => l.Book != null)
                .GroupBy(l => l.Book.Author)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .ToList();

            return books
                .Where(b => popularAuthors.Contains(b.Author) && b.NoOfAvailableCopies > 0)
                .ToList();
        }

        
        public static IEnumerable<Book> GetSeasonalRecommendations(IEnumerable<Book> books, IEnumerable<Lending> lendings)
        {
            int currentMonth = DateTime.Now.Month;

            return lendings
                .Where(l => l.LendingPeriodStart.Month == currentMonth)
                .GroupBy(l => l.BookId)
                .OrderByDescending(g => g.Count())
                .Select(g => books.FirstOrDefault(b => b.Id == g.Key))
                .Where(b => b != null && b.NoOfAvailableCopies > 0)
                .Take(5)
                .ToList();
        }

        public static float GetPreditionOfLendingABook(Book book,IEnumerable<Lending> lendings)
        {
            return LendingPrediction.PredictForThisMonth(book, lendings);
        }
    }
}
