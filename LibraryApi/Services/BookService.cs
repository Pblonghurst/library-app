using LibraryApi.Models;

namespace LibraryApi.Services;

public class BookService
{
    // private fields
    private readonly List<Book> _books = new();
    private int _nextId = 16;

    // constructor
    public BookService()
    {
        var seed = new[]
        {
            new Book { Id = 1, Title = "1984", Author = "George Orwell", Rating = 5, Comments = "A classic dystopian novel.", Isbn = "9780451524935" },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Rating = 5, Comments = "Powerful story about racial injustice.", Isbn = "9780061120084" },
            new Book { Id = 3, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Rating = 4, Comments = "Jazz age tragedy.", Isbn = "9780743273565" },
            new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Rating = 5, Comments = "Timeless romance.", Isbn = "9780141439518" },
            new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Rating = 4, Comments = "Coming of age.", Isbn = "9780316769488" },
            new Book { Id = 6, Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", Rating = 5, Comments = "Magical start to the series.", Isbn = "9780747532699" },
            new Book { Id = 7, Title = "The Hobbit", Author = "J.R.R. Tolkien", Rating = 5, Comments = "Adventure in Middle-earth.", Isbn = "9780547928227" },
            new Book { Id = 8, Title = "Fahrenheit 451", Author = "Ray Bradbury", Rating = 4, Comments = "Books are forbidden.", Isbn = "9781451673319" },
            new Book { Id = 9, Title = "Animal Farm", Author = "George Orwell", Rating = 4, Comments = "Political allegory.", Isbn = "9780451526342" },
            new Book { Id = 10, Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Rating = 5, Comments = "Epic fantasy.", Isbn = "9780544003415" },
            new Book { Id = 11, Title = "Jane Eyre", Author = "Charlotte Brontë", Rating = 4, Comments = "Gothic romance.", Isbn = "9780141441146" },
            new Book { Id = 12, Title = "The Da Vinci Code", Author = "Dan Brown", Rating = 3, Comments = "Thriller with puzzles.", Isbn = "9780307474278" },
            new Book { Id = 13, Title = "The Alchemist", Author = "Paulo Coelho", Rating = 4, Comments = "Quest for destiny.", Isbn = "9780062315007" },
            new Book { Id = 14, Title = "Brave New World", Author = "Aldous Huxley", Rating = 4, Comments = "Dystopian future.", Isbn = "9780060850524" },
            new Book { Id = 15, Title = "The Hitchhiker's Guide to the Galaxy", Author = "Douglas Adams", Rating = 5, Comments = "Comic sci-fi.", Isbn = "9780345391803" },
        };
        _books.AddRange(seed);
    }

    // get all books
    public IReadOnlyList<Book> GetAll() => _books.AsReadOnly();

    // get a page of books (1-based page), optional search and sort (title, author, isbn)
    public (IReadOnlyList<Book> Items, int Total) GetPage(int page, int pageSize, string? search = null, string? sortBy = null)
    {
        var query = _books.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLowerInvariant();
            query = query.Where(b =>
                (b.Title?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false) ||
                (b.Author?.Contains(term, StringComparison.OrdinalIgnoreCase) ?? false));
        }
        var sort = sortBy?.Trim().ToLowerInvariant();
        if (sort == "title")
            query = query.OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase);
        else if (sort == "author")
            query = query.OrderBy(b => b.Author, StringComparer.OrdinalIgnoreCase);
        else if (sort == "isbn")
            query = query.OrderBy(b => b.Isbn ?? "", StringComparer.Ordinal);
        else if (sort == "rating")
            query = query.OrderByDescending(b => b.Rating);
        var list = query.ToList();
        var total = list.Count;
        var skip = Math.Max(0, (page - 1) * pageSize);
        var items = list.Skip(skip).Take(pageSize).ToList();
        return (items, total);
    }

    // get a book by id
    public Book? GetById(int id) => _books.Find(b => b.Id == id);

    // current book count (for max-books validation)
    public int Count => _books.Count;

    // add a book
    public Book Add(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book;
    }

    // update a book
    public Book Update(Book book)
    {
        var existingBook = _books.Find(b => b.Id == book.Id);
        if (existingBook == null)
        {
            throw new Exception("Book not found");
        }
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Rating = book.Rating;
        existingBook.Comments = book.Comments;
        existingBook.Isbn = book.Isbn;
        return existingBook;
    }

    // delete a book
    public void Delete(int id)
    {
        var book = _books.Find(b => b.Id == id);
        if (book != null)
            _books.Remove(book);
    }
}
