using Microsoft.EntityFrameworkCore;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class LibraryConsoleApp : IApp
    {
        private readonly IService _service;

        public LibraryConsoleApp(IService service)
        {
            _service = service;
        }

        public void Run()
        {
            while (true)
            {
                PrintMenu();
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1: ShowBooks(); break;
                        case 2: SearchBook(); break;
                        case 3: AddBook(); break;
                        case 4: DeleteBook(); break;
                        case 5: UpdateBook(); break;
                        case 6: AddCopiesOfBook(); break;
                        case 7: DeleteCopiesOfBook(); break;
                        case 8: LendBook(); break;
                        case 9: ReturnBook(); break;
                        case 10: BooksRecommendation(); break;
                        case 11: Stop(); break;
                        default: Console.WriteLine("Invalid Option"); break;

                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

        }

        private void ShowBooks()
        {
            IEnumerable<Book> books = _service.GetAllBooks();
            Console.WriteLine("\nBooks:\n");
            books.ToList().ForEach(book => Console.WriteLine(book.ToString()));
        }

        private void SearchBook()
        {
            while (true)
            {
                Console.WriteLine("\n Search Book(s) By:");
                Console.WriteLine("1. Title");
                Console.WriteLine("2. Author");
                Console.WriteLine("3. ISBN");
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            {
                                Console.WriteLine("\nEnter Title: ");
                                var title = Console.ReadLine();
                                if (title != null)
                                {
                                    Book book = new Book(title, null, 0, 0, null);
                                    IEnumerable<Book> books = _service.SearchBooks(book);
                                    if (books.Count() == 0)
                                    {
                                        Console.WriteLine("There are no books with this title");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nBooks:\n");
                                        books.ToList().ForEach(books => Console.WriteLine(books.ToString()));
                                    }
                                }
                                return;
                            }
                        case 2:
                            {
                                Console.WriteLine("\nEnter Author: ");
                                var author = Console.ReadLine();
                                if (author != null)
                                {
                                    Book book = new Book(null, author, 0, 0, null);
                                    IEnumerable<Book> books = _service.SearchBooks(book);
                                    if (books.Count() == 0)
                                    {
                                        Console.WriteLine("There are no books with this author");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nBooks:\n");
                                        books.ToList().ForEach(books => Console.WriteLine(books.ToString()));
                                    }
                                }
                                return;
                            }
                        case 3:
                            {
                                Console.WriteLine("\nEnter ISBN: ");
                                var ISBN = Console.ReadLine();
                                if (ISBN != null)
                                {
                                    Book book = new Book(null, null, 0, 0, ISBN);
                                    IEnumerable<Book> books = _service.SearchBooks(book);
                                    if (books.Count() == 0)
                                    {
                                        Console.WriteLine("There are no books with this ISBN");
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nBooks:\n");
                                        books.ToList().ForEach(books => Console.WriteLine(books.ToString()));
                                    }
                                }
                                return;
                            }
                        default: Console.WriteLine("Invalid input"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void AddBook()
        {
            try
            {
                Console.WriteLine("\nEnter Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("Enter Author : ");
                var author = Console.ReadLine();
                Console.WriteLine("Enter ISBN : ");
                var isbn = Console.ReadLine();
                Console.WriteLine("Enter Quantity : ");
                var quantity = int.Parse(Console.ReadLine());
                Book book = new Book(title, author, quantity, quantity, isbn);
                _service.AddBook(book);
                Console.WriteLine("\nBook added successfully !");
          
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void DeleteBook()
        {
            try
            {
                Console.WriteLine("\nEnter Title : ");
                var title = Console.ReadLine();
                if (!string.IsNullOrEmpty(title))
                {
                    Book book = new Book(title);
                    _service.DeleteBook(book);
                    Console.WriteLine("\nBook deleted successfully !");
                }
                else Console.WriteLine("\nInvalid input");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void UpdateBook()
        {
            try
            {
                Console.WriteLine("\nEnter Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("Enter Author : ");
                var author = Console.ReadLine();
                Console.WriteLine("Enter ISBN : ");
                var isbn = Console.ReadLine();
                Console.WriteLine("Enter Quantity : ");
                var quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter No. Of Avalaible Copies : ");
                var availableCopies = int.Parse(Console.ReadLine());
                Book book = new Book(title, author, quantity, availableCopies, isbn);
                _service.UpdateBook(book);
                Console.WriteLine("\nBook updated successfully!");
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void AddCopiesOfBook()
        {
            try
            {
                Console.WriteLine("\nEnter Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("Enter No. Of New Copies : ");
                var newCopies = int.Parse(Console.ReadLine());
                if (!string.IsNullOrEmpty(title) && newCopies > 0)
                {
                    Book book = new Book(title);
                    _service.AddBookCopies(book, newCopies);
                    Console.WriteLine("\nCopies added successfully !");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void DeleteCopiesOfBook()
        {
            try
            {
                Console.WriteLine("\nEnter Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("Enter No. Of Copies : ");
                var copies = int.Parse(Console.ReadLine());
                if (!string.IsNullOrEmpty(title) && copies > 0)
                {
                    Book book = new Book(title);
                    _service.RemoveBookCopies(book, copies);
                    Console.WriteLine("\nCopies removed successfully");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void BooksRecommendation()
        {
            while (true)
            {
                Console.WriteLine("\nShow Books Recommendations:");
                Console.WriteLine("1. Get Top 5 Lent Books");
                Console.WriteLine("2. Get By Authors Popularity");
                Console.WriteLine("3. Get Top 5 Books For This Season");
                Console.WriteLine("4. Get Predition of Lending For This Month");
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            {
                                Console.WriteLine("\nTop 5 Lent Books : ");
                                var books = _service.GetMostLentBooks(5);
                                books.ToList().ForEach(book => Console.WriteLine(book.ToString()));
                                return;
                            }
                        case 2:
                            {
                                Console.WriteLine("\nTop Books By Authors Popularity : ");
                                var books = _service.GetRecommendationsByAuthorPopularity();
                                books.ToList().ForEach(books => Console.WriteLine(books.ToString()));
                                return;
                            }
                        case 3:
                            {
                                Console.WriteLine("\nTop 5 Books For This Season : ");
                                var books = _service.GetSeasonalRecommendations();
                                books.ToList().ForEach(books => Console.WriteLine(books.ToString()));
                                return;
                            }
                        case 4:
                            {
                                Console.WriteLine("\nEnter Book Title : ");
                                var title = Console.ReadLine();

                                if (!string.IsNullOrEmpty(title))
                                {
                                    Book book = new Book(title);
                                    float predictionScore = _service.GetPreditionOfLendingABook(book);
                                    Console.WriteLine($"""Expected loans for "{title}" this month: {predictionScore} copies.""");
                                }
                                return;
                            }
                        default: Console.WriteLine("Invalid input"); break;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        private void LendBook()
        {
            try
            {
                Console.WriteLine("\nEnter Book Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("\nEnter Client's Name : ");
                var clientName = Console.ReadLine();
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(clientName))
                {
                    Book book = new Book(title);
                    Lending lending = new Lending(book, clientName, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(14), LendingStatus.BookLent);
                    _service.LendBook(lending);
                    Console.WriteLine("\nBook lent successfully !");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void ReturnBook()
        {
            try
            {
                Console.WriteLine("\nEnter Book Title : ");
                var title = Console.ReadLine();
                Console.WriteLine("\nEnter Client's Name : ");
                var clientName = Console.ReadLine();
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(clientName))
                {
                    Book book = new Book(title);
                    Lending lending = new Lending(book, clientName);
                    _service.ReturnBook(lending);
                    Console.WriteLine("\nBook returned successfully !");
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void Stop()
        {
            Environment.Exit(0);
        }

        private void PrintMenu()
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Show Books");
            Console.WriteLine("2. Search book");
            Console.WriteLine("3. Add book");
            Console.WriteLine("4. Delete book");
            Console.WriteLine("5. Update book");
            Console.WriteLine("6. Add copies of book");
            Console.WriteLine("7. Remove copies of book");
            Console.WriteLine("8. Lend book");
            Console.WriteLine("9. Return book");
            Console.WriteLine("10. Books Recommendations");
            Console.WriteLine("11. Exit\n");
        }
    }
}
