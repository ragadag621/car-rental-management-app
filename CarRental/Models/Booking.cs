using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

/// <summary>
/// Captures the raw user input for a rental booking.
/// Data-annotation attributes drive the built-in Blazor form validation,
/// while deeper business rules (e.g. date ordering) live in the service layer.
/// </summary>
public class Booking
{
    /// <summary>Name of the customer making the booking.</summary>
    [Required(ErrorMessage = "Please enter your name.")]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>First day of the rental period.</summary>
    [Required(ErrorMessage = "A start date is required.")]
    public DateTime? StartDate { get; set; }

    /// <summary>Last day of the rental period.</summary>
    [Required(ErrorMessage = "An end date is required.")]
    public DateTime? EndDate { get; set; }
}

/// <summary>
/// Immutable result returned by the booking service once a booking
/// has been successfully validated and priced.
/// </summary>
public class BookingSummary
{
    public string CustomerName { get; init; } = string.Empty;
    public Car Car { get; init; } = new();
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    /// <summary>Total number of rental days (inclusive of both endpoints).</summary>
    public int NumberOfDays { get; init; }

    /// <summary>Final price: DailyPrice * NumberOfDays.</summary>
    public decimal TotalPrice { get; init; }
}

/// <summary>
/// Wraps the outcome of a booking attempt so the UI can react to
/// either a success (with a summary) or a validation failure (with a message).
/// </summary>
public class BookingResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public BookingSummary? Summary { get; init; }

    public static BookingResult Fail(string message) =>
        new() { Success = false, ErrorMessage = message };

    public static BookingResult Ok(BookingSummary summary) =>
        new() { Success = true, Summary = summary };
}
