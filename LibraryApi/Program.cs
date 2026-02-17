using System.Text.Json;
using LibraryApi.Models;
using LibraryApi.Services;
using LibraryApi.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<BookService>();

var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();

// get books (paginated: ?page=1&pageSize=10, optional ?search=..., ?sortBy=title|author|isbn|rating)
app.MapGet("/api/books", (int? page, int? pageSize, string? search, string? sortBy, BookService bookService) =>
{
    var p = Math.Max(1, page ?? 1);
    var ps = Math.Clamp(pageSize ?? 10, 1, 100);
    var (items, total) = bookService.GetPage(p, ps, search, sortBy);
    return Results.Ok(new { items, total });
});

// add a book (validates: max 25 books, rating 1-5, no "horrible" in comments, string length limits)
app.MapPost("/api/books", (Book book, BookService bookService) =>
{
    var validationErrors = BookValidation.ValidateBookCreate(book);
    if (validationErrors.Count > 0)
        return Results.BadRequest(new { message = "Validation failed.", errors = validationErrors });

    if (bookService.Count >= BookValidation.MaxBooks)
        return Results.BadRequest(new { message = $"Maximum {BookValidation.MaxBooks} books allowed. Cannot add more." });

    var added = bookService.Add(book);
    return Results.Created($"/api/books/{added.Id}", added);
});

// get one book (for update)
app.MapGet("/api/books/{id}", (int id, BookService bookService) =>
{
    var book = bookService.GetById(id);
    return book is null ? Results.NotFound(new { message = "Book not found." }) : Results.Ok(book);
});

// update a book (partial: rating, comments; validates rating 1-5, no "horrible", length)
app.MapPatch("/api/books/{id}", (int id, BookUpdate update, BookService bookService) =>
{
    var existing = bookService.GetById(id);
    if (existing is null)
        return Results.NotFound(new { message = "Book not found." });

    var validationErrors = BookValidation.ValidateBookUpdate(update.Rating, update.Comments);
    if (validationErrors.Count > 0)
        return Results.BadRequest(new { message = "Validation failed.", errors = validationErrors });

    existing.Rating = update.Rating;
    existing.Comments = update.Comments;
    var updated = bookService.Update(existing);
    return Results.Ok(updated);
});

// delete a book
app.MapDelete("/api/books/{id}", (int id, BookService bookService) =>
{
    var existing = bookService.GetById(id);
    if (existing is null)
        return Results.NotFound(new { message = "Book not found." });
    bookService.Delete(id);
    return Results.NoContent();
});

app.Run();

// DTO for PATCH /api/books/{id} (camelCase via JsonNamingPolicy)
record BookUpdate(int Rating, string? Comments);
