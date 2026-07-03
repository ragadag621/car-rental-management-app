using CarRental.Models;

namespace CarRental.Services;

/// <summary>
/// Abstraction for retrieving and filtering cars.
/// Depending on an interface (rather than a concrete class) keeps the UI
/// loosely coupled and makes the logic easy to unit test or swap out later.
/// </summary>
public interface ICarService
{
    /// <summary>Returns every car in the catalog.</summary>
    IReadOnlyList<Car> GetAllCars();

    /// <summary>
    /// Returns cars matching the given category, or all cars when
    /// <paramref name="category"/> is null.
    /// </summary>
    IReadOnlyList<Car> GetCarsByCategory(CarCategory? category);

    /// <summary>Looks up a single car by its identifier.</summary>
    Car? GetCarById(int id);

    /// <summary>Returns the distinct set of categories present in the catalog.</summary>
    IReadOnlyList<CarCategory> GetAvailableCategories();
}

/// <summary>
/// In-memory implementation of <see cref="ICarService"/>.
/// Data is stored in a hardcoded list; images ship inside wwwroot/images so
/// they never depend on an external network.
/// </summary>
public class CarService : ICarService
{
    private readonly List<Car> _cars = new()
    {
        new Car
        {
            Id = 1,
            Name = "Toyota Corolla",
            Brand = "Toyota",
            ImageUrl = "/images/toyota-corolla.png",
            DailyPrice = 45m,
            Category = CarCategory.Economy,
            Year = 2023,
            TopSpeedKmh = 180,
            Horsepower = 139,
            Seats = 5,
            Transmission = "Automatic",
            FuelType = "Petrol",
            AccentColor = "#22c55e",
            Description = "A reliable and fuel-efficient compact sedan, perfect for city driving and everyday commutes. Comfortable, economical, and easy to handle."
        },
        new Car
        {
            Id = 2,
            Name = "BMW 5 Series",
            Brand = "BMW",
            ImageUrl = "/images/bmw-5-series.png",
            DailyPrice = 120m,
            Category = CarCategory.Luxury,
            Year = 2024,
            TopSpeedKmh = 250,
            Horsepower = 335,
            Seats = 5,
            Transmission = "Automatic",
            FuelType = "Petrol",
            AccentColor = "#f59e0b",
            Description = "A refined executive sedan blending sporty performance with premium comfort. Elegant interior, advanced tech, and a smooth, powerful drive."
        },
        new Car
        {
            Id = 3,
            Name = "Porsche 911",
            Brand = "Porsche",
            ImageUrl = "/images/porsche-911.png",
            DailyPrice = 250m,
            Category = CarCategory.Sports,
            Year = 2024,
            TopSpeedKmh = 293,
            Horsepower = 379,
            Seats = 2,
            Transmission = "Automatic",
            FuelType = "Petrol",
            AccentColor = "#ef4444",
            Description = "An iconic sports car delivering breathtaking acceleration and razor-sharp handling. The ultimate driving experience for enthusiasts."
        },
        new Car
        {
            Id = 4,
            Name = "Jeep Wrangler",
            Brand = "Jeep",
            ImageUrl = "/images/jeep-wrangler.png",
            DailyPrice = 95m,
            Category = CarCategory.SUV,
            Year = 2023,
            TopSpeedKmh = 160,
            Horsepower = 285,
            Seats = 5,
            Transmission = "Automatic",
            FuelType = "Petrol",
            AccentColor = "#3b82f6",
            Description = "A rugged off-road SUV built for adventure. Go anywhere with confidence, whether it's rocky trails or sandy dunes."
        },
        new Car
        {
            Id = 5,
            Name = "Mercedes E-Class",
            Brand = "Mercedes-Benz",
            ImageUrl = "/images/mercedes-e-class.png",
            DailyPrice = 130m,
            Category = CarCategory.Luxury,
            Year = 2024,
            TopSpeedKmh = 240,
            Horsepower = 255,
            Seats = 5,
            Transmission = "Automatic",
            FuelType = "Diesel",
            AccentColor = "#f59e0b",
            Description = "A luxurious business sedan offering supreme comfort, cutting-edge safety, and a whisper-quiet cabin. Travel in style and sophistication."
        }
    };

    /// <inheritdoc />
    public IReadOnlyList<Car> GetAllCars() => _cars;

    /// <inheritdoc />
    public IReadOnlyList<Car> GetCarsByCategory(CarCategory? category)
    {
        if (category is null)
        {
            return _cars;
        }

        return _cars
            .Where(car => car.Category == category)
            .ToList();
    }

    /// <inheritdoc />
    public Car? GetCarById(int id) =>
        _cars.FirstOrDefault(car => car.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<CarCategory> GetAvailableCategories() =>
        _cars
            .Select(car => car.Category)
            .Distinct()
            .OrderBy(category => category.ToString())
            .ToList();
}
