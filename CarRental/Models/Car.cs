namespace CarRental.Models;

/// <summary>
/// Represents a single rentable car in the catalog.
/// Plain data model (POCO) with no UI or persistence logic.
/// </summary>
public class Car
{
    public int Id { get; set; }

    /// <summary>Display name of the car (e.g. "Toyota Corolla").</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Manufacturer brand (e.g. "Toyota").</summary>
    public string Brand { get; set; } = string.Empty;

    /// <summary>Relative path to the car's image inside wwwroot.</summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>Rental cost per day in USD.</summary>
    public decimal DailyPrice { get; set; }

    /// <summary>The category the car belongs to.</summary>
    public CarCategory Category { get; set; }

    // --- Detailed specifications shown on the car detail page ---

    /// <summary>Model / manufacturing year.</summary>
    public int Year { get; set; }

    /// <summary>Top speed in km/h.</summary>
    public int TopSpeedKmh { get; set; }

    /// <summary>Engine horsepower.</summary>
    public int Horsepower { get; set; }

    /// <summary>Number of seats.</summary>
    public int Seats { get; set; }

    /// <summary>Transmission type (e.g. "Automatic").</summary>
    public string Transmission { get; set; } = string.Empty;

    /// <summary>Fuel type (e.g. "Petrol", "Diesel", "Electric").</summary>
    public string FuelType { get; set; } = string.Empty;

    /// <summary>Marketing description of the car.</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Accent color (hex) used for the category badge / placeholder.</summary>
    public string AccentColor { get; set; } = "#f59e0b";
}

/// <summary>
/// Enumerates the available car categories.
/// Strongly-typed to prevent typos and enable LINQ filtering.
/// </summary>
public enum CarCategory
{
    Economy,
    Sports,
    SUV,
    Luxury
}
