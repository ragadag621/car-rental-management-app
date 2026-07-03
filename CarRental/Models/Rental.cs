namespace CarRental.Models;

/// <summary>
/// A confirmed rental that belongs to a specific user.
/// Persisted (in memory) so the customer can see it under "My Rentals".
/// </summary>
public class Rental
{
    public int Id { get; set; }

    /// <summary>Owner of this rental.</summary>
    public int UserId { get; set; }

    /// <summary>The rented car (denormalized snapshot reference by id).</summary>
    public int CarId { get; set; }
    public string CarName { get; set; } = string.Empty;
    public string CarImageUrl { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfDays { get; set; }
    public decimal TotalPrice { get; set; }

    /// <summary>When the booking was created.</summary>
    public DateTime BookedOn { get; set; } = DateTime.Now;

    /// <summary>Computed status based on today's date.</summary>
    public string Status
    {
        get
        {
            var today = DateTime.Today;
            if (today < StartDate.Date) return "Upcoming";
            if (today > EndDate.Date) return "Completed";
            return "Active";
        }
    }
}
