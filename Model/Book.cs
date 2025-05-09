using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int NoOfAvailableCopies { get; set; }
        public string ISBN { get; set; }
        public List<Lending> Lendings { get; set; }
        public Book() { }
        public Book(int id, string title, string author, int quantity, int noOfAvailableCopies,string isbn)
        {
            Id = id; Title = title; Author = author; Quantity = quantity;
            NoOfAvailableCopies = noOfAvailableCopies;
            ISBN = isbn;
        }
        
        public Book(string title, string author, int quantity,int noOfAvailableCopies,string isbn)
        {
            Title = title; Author = author; Quantity = quantity;
            NoOfAvailableCopies = noOfAvailableCopies;
            ISBN = isbn;
        }

        public Book(string title) {  Title = title; }
        

        public override string ToString()
        {
            return "Book { Title = " + Title + "; Author = " + Author + "; ISBN = " + ISBN + "; Available Copies = " + NoOfAvailableCopies+ "; Quantity = " + Quantity + " }";
        }
    }
}
