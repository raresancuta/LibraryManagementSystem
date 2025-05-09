using Persistence;
using Persistence.Database;
using Service;
using UI;

public class Program
{
    public static void Main(string[] args)
    {
        IBookRepository _bookRepository = new BookDBRepository();
        ILendingRepository _lendingRepository = new LendingDBRepository();
        IService _service = new LibraryService(_bookRepository, _lendingRepository);
        IApp app = new LibraryConsoleApp(_service);
        app.Run();
    }
}