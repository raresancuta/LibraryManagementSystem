using Model;
using Persistence.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class LendingSeeder
    {
        private static readonly Random random = new Random();

        private static readonly List<string> ClientNames = new List<string>
        {
        "Andrei Popescu", "Maria Ionescu", "Ioana Georgescu", "Vlad Stan", "Elena Dobre",
        "Cristian Marin", "Ana Iliescu", "Paul Radu", "Mihai Ene", "Laura Dumitrescu", 
        "Alina Maria", "Nicolae Irimia", "Alexandru Ceusan"
        };

        public static void GenerateLendingsForLast6Years()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly startDate = today.AddYears(-6).AddDays(-today.Day + 1); 

            var booksRepo = new BookDBRepository();
            var books = booksRepo.FindAll();
            var lendingRepo = new LendingDBRepository();

            for (DateOnly month = startDate; month <= today; month = month.AddMonths(1))
            {
                foreach (var book in books)
                {
                    int numberOfLendings = random.Next(0, book.Quantity + 1); 

                    for (int i = 0; i < numberOfLendings; i++)
                    {
                        int day = random.Next(1, DateTime.DaysInMonth(month.Year, month.Month) + 1);
                        DateOnly lendingStart = new DateOnly(month.Year, month.Month, day);
                        DateOnly lendingEnd = lendingStart.AddDays(random.Next(7, 31)); 

                        string clientName = ClientNames[random.Next(ClientNames.Count)];

                        Lending lending = new Lending(book,clientName,lendingStart,lendingEnd,LendingStatus.BookLent);

                        lendingRepo.Save(lending);
                    }
                }
            }
        }
    }
}
