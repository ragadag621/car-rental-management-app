namespace CarRental.Models;

/// <summary>
/// Represents a single rentable car in the catalog.
/// This is a plain data model (POCO) and contains no UI or persistence logic,
/// keeping the domain separate from the presentation layer.
/// </summary>
public class Car
{
    /// <summary>Unique identifier for the car.</summary>
    public int Id { get; set; }

    /// <summary>Display name of the car (e.g. "Toyota Corolla").</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Absolute or relative URL to the car's image.</summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>Rental cost per day in USD.</summary>
    public decimal DailyPrice { get; set; }

    /// <summary>The category the car belongs to (Economy, Sports, SUV, ...).</summary>
    public CarCategory Category { get; set; }
}

/// <summary>
/// Enumerates the available car categories.
/// Using an enum instead of raw strings prevents typos and enables
/// strongly-typed LINQ filtering.
/// </summary>
public enum CarCategory
{
    Economy,
    Sports,
    SUV,
    Luxury
}
