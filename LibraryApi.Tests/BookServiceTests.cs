using LibraryApi.Models;
using LibraryApi.Services;
using Xunit;

namespace LibraryApi.Tests;

public class BookServiceTests
{
    [Fact]
    public void GetAll_ReturnsSeededBooks()
    {
        // Arrange
        var service = new BookService();

        // Act
        var books = service.GetAll();

        // Assert
        Assert.NotEmpty(books);
        Assert.Equal(15, books.Count); // 15 seeded books
    }

    [Fact]
    public void GetById_ReturnsCorrectBook()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = service.GetById(1);

        // Assert
        Assert.NotNull(book);
        Assert.Equal("1984", book.Title);
        Assert.Equal("George Orwell", book.Author);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenBookDoesNotExist()
    {
        // Arrange
        var service = new BookService();

        // Act
        var book = service.GetById(999);

        // Assert
        Assert.Null(book);
    }

    [Fact]
    public void Add_CreatesNewBook_WithNewId()
    {
        // Arrange
        var service = new BookService();
        var newBook = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            Rating = 4,
            Comments = "Great test book"
        };

        // Act
        var addedBook = service.Add(newBook);

        // Assert
        Assert.Equal(16, addedBook.Id); // Next ID after seeded books
        Assert.Equal("Test Book", addedBook.Title);
        Assert.Equal(16, service.Count);
    }

    [Fact]
    public void Delete_RemovesBook()
    {
        // Arrange
        var service = new BookService();
        var initialCount = service.Count;

        // Act
        service.Delete(1);

        // Assert
        Assert.Equal(initialCount - 1, service.Count);
        Assert.Null(service.GetById(1));
    }

    [Fact]
    public void Update_ModifiesExistingBook()
    {
        // Arrange
        var service = new BookService();
        var bookToUpdate = new Book
        {
            Id = 1,
            Title = "Updated Title",
            Author = "Updated Author",
            Rating = 3,
            Comments = "Updated comments"
        };

        // Act
        var updatedBook = service.Update(bookToUpdate);

        // Assert
        Assert.Equal("Updated Title", updatedBook.Title);
        Assert.Equal("Updated Author", updatedBook.Author);
        Assert.Equal(3, updatedBook.Rating);
    }

    [Fact]
    public void GetPage_ReturnsPaginatedResults()
    {
        // Arrange
        var service = new BookService();

        // Act
        var (items, total) = service.GetPage(page: 1, pageSize: 5);

        // Assert
        Assert.Equal(5, items.Count);
        Assert.Equal(15, total);
    }

    [Fact]
    public void GetPage_WithSearch_FiltersResults()
    {
        // Arrange
        var service = new BookService();

        // Act
        var (items, total) = service.GetPage(page: 1, pageSize: 10, search: "Orwell");

        // Assert
        Assert.Equal(2, total); // "1984" and "Animal Farm" by George Orwell
        Assert.All(items, book => Assert.Contains("Orwell", book.Author));
    }
}
