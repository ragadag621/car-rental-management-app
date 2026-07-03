using CarRental.Models;

namespace CarRental.Services;

/// <summary>
/// Holds the currently signed-in user for the active Blazor circuit.
/// Registered as Scoped so each user's connection has its own session.
/// Note: state lives in server memory and is lost on refresh/restart
/// (a production app would persist auth via cookies/JWT).
/// </summary>
public class UserSession
{
    public User? CurrentUser { get; private set; }

    public bool IsLoggedIn => CurrentUser is not null;

    /// <summary>Raised whenever the login state changes so the UI can refresh.</summary>
    public event Action? OnChange;

    public void SignIn(User user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}
