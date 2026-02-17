namespace LibraryApi.Validation;

/// <summary>
/// Business rules: max 25 books, rating 1–5, "horrible" disallowed in comments, sensible string length limits.
/// </summary>
public static class BookValidation
{
    public const int MaxBooks = 25;
    public const int RatingMin = 1;
    public const int RatingMax = 5;
    public const int MaxTitleLength = 200;
    public const int MaxAuthorLength = 200;
    public const int MaxCommentsLength = 500;
    public const int MaxIsbnLength = 20;
    private const string ForbiddenWord = "horrible";

    /// <summary>Validates a book for create (POST). Returns list of error messages; empty if valid.</summary>
    public static IReadOnlyList<string> ValidateBookCreate(Models.Book book)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(book.Title))
            errors.Add("Title is required.");
        else if (book.Title.Length > MaxTitleLength)
            errors.Add($"Title must be at most {MaxTitleLength} characters.");

        if (string.IsNullOrWhiteSpace(book.Author))
            errors.Add("Author is required.");
        else if (book.Author.Length > MaxAuthorLength)
            errors.Add($"Author must be at most {MaxAuthorLength} characters.");

        if (book.Rating < RatingMin || book.Rating > RatingMax)
            errors.Add($"Rating must be between {RatingMin} and {RatingMax}.");

        if (book.Comments != null)
        {
            if (book.Comments.Length > MaxCommentsLength)
                errors.Add($"Comments must be at most {MaxCommentsLength} characters.");
            if (ContainsForbiddenWord(book.Comments))
                errors.Add("The word \"horrible\" is not allowed in comments.");
        }

        if (book.Isbn != null && book.Isbn.Length > MaxIsbnLength)
            errors.Add($"ISBN must be at most {MaxIsbnLength} characters.");

        return errors;
    }

    /// <summary>Validates a book update (PATCH: rating and comments). Returns list of error messages; empty if valid.</summary>
    public static IReadOnlyList<string> ValidateBookUpdate(int rating, string? comments)
    {
        var errors = new List<string>();

        if (rating < RatingMin || rating > RatingMax)
            errors.Add($"Rating must be between {RatingMin} and {RatingMax}.");

        if (comments != null)
        {
            if (comments.Length > MaxCommentsLength)
                errors.Add($"Comments must be at most {MaxCommentsLength} characters.");
            if (ContainsForbiddenWord(comments))
                errors.Add("The word \"horrible\" is not allowed in comments.");
        }

        return errors;
    }

    private static bool ContainsForbiddenWord(string text)
    {
        return text.Contains(ForbiddenWord, StringComparison.OrdinalIgnoreCase);
    }
}
