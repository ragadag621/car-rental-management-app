using CarRental.Models;

namespace CarRental.Services;

/// <summary>
/// Stores confirmed rentals per user (in memory) and exposes them for the
/// "My Rentals" page. Singleton so bookings persist across the app lifetime.
/// </summary>
public interface IRentalService
{
    /// <summary>Persists a booking summary as a rental owned by the given user.</summary>
    Rental AddRental(int userId, BookingSummary summary);

    /// <summary>Returns all rentals for a user, newest first.</summary>
    IReadOnlyList<Rental> GetRentalsForUser(int userId);
}

/// <inheritdoc />
public class RentalService : IRentalService
{
    private readonly List<Rental> _rentals = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public Rental AddRental(int userId, BookingSummary summary)
    {
        lock (_lock)
        {
            var rental = new Rental
            {
                Id = _nextId++,
                UserId = userId,
                CarId = summary.Car.Id,
                CarName = summary.Car.Name,
                CarImageUrl = summary.Car.ImageUrl,
                StartDate = summary.StartDate,
                EndDate = summary.EndDate,
                NumberOfDays = summary.NumberOfDays,
                TotalPrice = summary.TotalPrice,
                BookedOn = DateTime.Now
            };

            _rentals.Add(rental);
            return rental;
        }
    }

    public IReadOnlyList<Rental> GetRentalsForUser(int userId)
    {
        lock (_lock)
        {
            return _rentals
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.BookedOn)
                .ToList();
        }
    }
}
