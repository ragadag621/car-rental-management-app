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
/// Data is stored in a hardcoded <see cref="List{Car}"/> as required;
/// no database or file storage is used.
/// </summary>
public class CarService : ICarService
{
    // The single source of truth for the catalog. Marked readonly so the
    // reference cannot be reassigned after construction.
    private readonly List<Car> _cars = new()
    {
        new Car
        {
            Id = 1,
            Name = "Toyota Corolla",
            ImageUrl = "https://images.unsplash.com/photo-1623869675781-80aa31012a5a?w=800&q=80",
            DailyPrice = 45m,
            Category = CarCategory.Economy
        },
        new Car
        {
            Id = 2,
            Name = "Porsche 911",
            ImageUrl = "https://images.unsplash.com/photo-1503376780353-7e6692767b70?w=800&q=80",
            DailyPrice = 320m,
            Category = CarCategory.Sports
        },
        new Car
        {
            Id = 3,
            Name = "Jeep Grand Cherokee",
            ImageUrl = "https://images.unsplash.com/photo-1533473359331-0135ef1b58bf?w=800&q=80",
            DailyPrice = 110m,
            Category = CarCategory.SUV
        },
        new Car
        {
            Id = 4,
            Name = "Mercedes-Benz S-Class",
            ImageUrl = "https://images.unsplash.com/photo-1618843479313-40f8afb4b4d8?w=800&q=80",
            DailyPrice = 250m,
            Category = CarCategory.Luxury
        },
        new Car
        {
            Id = 5,
            Name = "Ford Mustang GT",
            ImageUrl = "https://images.unsplash.com/photo-1584345604476-8ec5e12e42dd?w=800&q=80",
            DailyPrice = 180m,
            Category = CarCategory.Sports
        }
    };

    /// <inheritdoc />
    public IReadOnlyList<Car> GetAllCars() => _cars;

    /// <inheritdoc />
    public IReadOnlyList<Car> GetCarsByCategory(CarCategory? category)
    {
        // When no category is selected we return the full list.
        if (category is null)
        {
            return _cars;
        }

        // LINQ filtering: keep only cars whose category matches the request.
        return _cars
            .Where(car => car.Category == category)
            .ToList();
    }

    /// <inheritdoc />
    public Car? GetCarById(int id) =>
        // LINQ single-or-default lookup by primary key.
        _cars.FirstOrDefault(car => car.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<CarCategory> GetAvailableCategories() =>
        // Project to category, de-duplicate, and order for a stable UI.
        _cars
            .Select(car => car.Category)
            .Distinct()
            .OrderBy(category => category.ToString())
            .ToList();
}
