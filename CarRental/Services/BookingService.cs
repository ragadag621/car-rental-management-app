using CarRental.Models;

namespace CarRental.Services;

/// <summary>
/// Abstraction for the booking/pricing workflow.
/// Keeping this behind an interface enforces the separation between
/// business logic and the Blazor UI.
/// </summary>
public interface IBookingService
{
    /// <summary>
    /// Validates the supplied booking against the chosen car and,
    /// if valid, calculates the total price and returns a summary.
    /// </summary>
    BookingResult CreateBooking(Car car, Booking booking);
}

/// <summary>
/// Encapsulates all booking rules: input validation, day calculation,
/// and total price computation. Contains no UI concerns.
/// </summary>
public class BookingService : IBookingService
{
    /// <inheritdoc />
    public BookingResult CreateBooking(Car car, Booking booking)
    {
        // --- Guard clauses / validation -----------------------------------

        if (car is null)
        {
            return BookingResult.Fail("No car was selected.");
        }

        if (string.IsNullOrWhiteSpace(booking.CustomerName))
        {
            return BookingResult.Fail("Customer name is required.");
        }

        if (booking.StartDate is null || booking.EndDate is null)
        {
            return BookingResult.Fail("Both start and end dates are required.");
        }

        // Core rule: the end date must be strictly after the start date.
        if (booking.EndDate <= booking.StartDate)
        {
            return BookingResult.Fail("End date must be after the start date.");
        }

        // Optional sensible rule: cannot book in the past.
        if (booking.StartDate < DateTime.Today)
        {
            return BookingResult.Fail("Start date cannot be in the past.");
        }

        // --- Calculation ---------------------------------------------------

        // Number of rental days = difference between the two dates.
        int numberOfDays = (booking.EndDate.Value.Date - booking.StartDate.Value.Date).Days;

        // Total price formula required by the spec: DailyPrice * NumberOfDays.
        decimal totalPrice = car.DailyPrice * numberOfDays;

        // --- Build the immutable summary ----------------------------------

        var summary = new BookingSummary
        {
            CustomerName = booking.CustomerName.Trim(),
            Car = car,
            StartDate = booking.StartDate.Value,
            EndDate = booking.EndDate.Value,
            NumberOfDays = numberOfDays,
            TotalPrice = totalPrice
        };

        return BookingResult.Ok(summary);
    }
}
