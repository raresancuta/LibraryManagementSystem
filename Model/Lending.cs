using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Lending
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
        public Book Book { get; set; }
        public string ClientName { get; set; }
        public DateOnly LendingPeriodStart { get; set; }
        public DateOnly LendingPeriodEnd { get; set; }
        public LendingStatus LendingStatus { get; set; }
        public Lending() { }
        public Lending(int id,Book book,string clientName,DateOnly start, DateOnly end,LendingStatus lendingStatus) {
            Id = id;
            Book = book;
            BookId = book.Id;
            ClientName = clientName;
            LendingPeriodStart = start;
            LendingPeriodEnd = end;
            LendingStatus = lendingStatus;
        }

        public Lending(Book book, string clientName, DateOnly start, DateOnly end,LendingStatus lendingStatus)
        {
            Book = book;
            BookId= book.Id;
            ClientName = clientName;
            LendingPeriodStart = start;
            LendingPeriodEnd = end;
            LendingStatus = lendingStatus;
        }

        public Lending(Book book,string clientName)
        {
            Book = book;
            ClientName = clientName;
        }

    }
}
