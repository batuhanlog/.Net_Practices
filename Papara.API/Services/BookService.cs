using Papara.API.Models;

public class BookService : IBookService
{
    private List<Book> _books;

    public BookService()
    {
        _books = new List<Book>(){
            new Book { Id = 1, Title = "a", Author = "Author 1", Price = 9.99m },
            new Book { Id = 2, Title = "c", Author = "Author 2", Price = 19.99m },
            new Book { Id = 3, Title = "b", Author = "Author 3", Price = 25.99m }

        };
    }

    public List<Book> GetAll()
    {
        return _books.ToList();
    }

    public Book GetById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public Book Create(Book newBook)
    {
        newBook.Id = _books.Max(b => b.Id) + 1;
        _books.Add(newBook);
        return newBook;
    }

    public Book Update(int id, Book updatedBook)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return null;
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Price = updatedBook.Price;
        return book;
    }

    public void Delete(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }

   public IEnumerable<Book> List(string title, string author, decimal? price)
    {
        var query = _books.AsQueryable();

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(b => b.Title.Contains(title));
        }

        if (!string.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Author.Contains(author));
        }

        if (price.HasValue)
        {
            query = query.Where(b => b.Price == price.Value);
        }

        return query.ToList();
    }

    public IEnumerable<Book> Sort(string orderBy)
    {
        var query = _books.AsQueryable();

        switch (orderBy.ToLower())
        {
            case "title":
                query = query.OrderBy(b => b.Title);
                break;
            case "author":
                query = query.OrderBy(b => b.Author);
                break;
            case "price":
                query = query.OrderBy(b => b.Price);
                break;
            default:
                break;
        }

        return query.ToList();
    }
}

