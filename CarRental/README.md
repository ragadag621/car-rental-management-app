# DriveNow — Car Rental (ASP.NET Core Blazor)

A small car-rental management app built with C# and Blazor Server, following clean-architecture
basics: the domain (models) and business logic (services) are fully separated from the UI.

## Features
- `Car` model with `Id`, `Name`, `ImageUrl`, `DailyPrice`, `Category`.
- Hardcoded in-memory catalog of 5 cars (no database / file storage).
- Category filtering using **LINQ** (`Where`, `Select`, `Distinct`).
- Booking workflow: name + start/end dates → validation → `DailyPrice * NumberOfDays`.
- Date validation: end date must be after start date, no past dates.
- Booking summary on success.
- Modern dark theme with Tailwind CSS, responsive grid layout.

## Architecture
```
Models/      → POCO domain types (Car, Booking, BookingSummary, BookingResult)
Services/    → ICarService/CarService, IBookingService/BookingService (all business logic)
Components/  → Blazor UI (Pages, Shared, Layout) — no business logic
Program.cs   → DI registration + app pipeline
```
Services are injected into components via interfaces, so the UI depends on abstractions only.

## Run it locally
Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download).

```bash
cd CarRental
dotnet restore
dotnet run
```
Then open the URL printed in the console (e.g. https://localhost:5001).

> Note: This app runs on the .NET runtime and cannot be previewed inside v0's
> Node.js environment — run it locally or deploy to any .NET host.
