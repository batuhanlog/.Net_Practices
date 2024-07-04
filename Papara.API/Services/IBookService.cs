using Papara.API.Models;

public interface IBookService
{
    List<Book> GetAll();
    Book GetById(int id);
    Book Create(Book newBook);
    Book Update(int id, Book updatedBook);
    void Delete(int id);
    IEnumerable<Book> List(string title, string author, decimal? price);
    IEnumerable<Book> Sort(string orderBy);
}
