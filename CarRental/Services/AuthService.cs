using System.Security.Cryptography;
using System.Text;
using CarRental.Models;

namespace CarRental.Services;

/// <summary>
/// Handles user registration and credential verification against an
/// in-memory user store. Passwords are salted and hashed with SHA-256;
/// plain-text passwords are never stored.
/// </summary>
public interface IAuthService
{
    AuthResult Register(RegisterModel model);
    AuthResult Login(LoginModel model);
    User? GetById(int id);
}

/// <summary>
/// Singleton, in-memory implementation. Data resets when the app restarts.
/// Swap this out for a database-backed store to make accounts persistent.
/// </summary>
public class AuthService : IAuthService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;
    private readonly object _lock = new();

    public AuthResult Register(RegisterModel model)
    {
        lock (_lock)
        {
            var email = model.Email.Trim().ToLowerInvariant();

            if (_users.Any(u => u.Email == email))
            {
                return AuthResult.Fail("An account with this email already exists.");
            }

            var user = new User
            {
                Id = _nextId++,
                FullName = model.FullName.Trim(),
                Email = email,
                PasswordHash = HashPassword(model.Password)
            };

            _users.Add(user);
            return AuthResult.Ok(user);
        }
    }

    public AuthResult Login(LoginModel model)
    {
        lock (_lock)
        {
            var email = model.Email.Trim().ToLowerInvariant();
            var user = _users.FirstOrDefault(u => u.Email == email);

            if (user is null || user.PasswordHash != HashPassword(model.Password))
            {
                return AuthResult.Fail("Invalid email or password.");
            }

            return AuthResult.Ok(user);
        }
    }

    public User? GetById(int id)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }

    /// <summary>Deterministic salted SHA-256 hash of a password.</summary>
    private static string HashPassword(string password)
    {
        const string salt = "CarRental::static-salt::v1";
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(salt + password);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
