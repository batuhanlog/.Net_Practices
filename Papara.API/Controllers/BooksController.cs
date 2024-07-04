using Papara.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
   public class BooksController : ControllerBase
{
    private readonly IBookService _bookService; // interface => Referans tutucu

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll() 
    {
        return Ok(_bookService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        var book = _bookService.GetById(id);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> Create([FromBody] Book newBook)
    {
        if(ModelState.IsValid){
            return BadRequest(new { message = "ID Required" });
        }

        if (newBook == null || string.IsNullOrEmpty(newBook.Title) || string.IsNullOrEmpty(newBook.Author) || newBook.Price <= 0)
        {
            return BadRequest(new { message = "Invalid book data" });
        }

        var createdBook = _bookService.Create(newBook);
        return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
    }

    [HttpPut("{id}")]
    public ActionResult<Book> Update(int id, [FromBody] Book updatedBook)
    {

        if(id != updatedBook.Id)
        {
            return BadRequest(new { message = "Id's dosn't match" });
        }

        var book = _bookService.Update(id, updatedBook);
        if (book == null)
        {
            return NotFound(new { message = "Book not found" });
        }

        if (updatedBook == null || string.IsNullOrEmpty(updatedBook.Title) || string.IsNullOrEmpty(updatedBook.Author) || updatedBook.Price <= 0)
        {
            return BadRequest(new { message = "Invalid book data" });
        }

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _bookService.Delete(id);
        return NoContent();
    }

     [HttpGet("list")]
    public ActionResult<IEnumerable<Book>> List([FromQuery] string title, [FromQuery] string author, [FromQuery] decimal? price)
    {
        var books = _bookService.List(title, author, price);
        return Ok(books);
    }

    [HttpGet("sort")]
    public ActionResult<IEnumerable<Book>> Sort([FromQuery] string orderBy)
    {
        var books = _bookService.Sort(orderBy);
        return Ok(books);
    }
}
}
