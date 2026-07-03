using CarRental.Components;
using CarRental.Services;

var builder = WebApplication.CreateBuilder(args);

// Register Blazor Server (interactive server-side rendering).
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- Dependency Injection ---------------------------------------------------
// Register the domain services against their interfaces so components depend
// on abstractions, not concrete implementations. CarService holds the in-memory
// catalog so it lives for the whole app (singleton); BookingService is stateless.
builder.Services.AddSingleton<ICarService, CarService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Auth + rentals: singletons hold the in-memory stores for the app lifetime,
// while UserSession is scoped to each user's Blazor circuit.
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IRentalService, RentalService>();
builder.Services.AddScoped<UserSession>();

var app = builder.Build();

// Standard pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
