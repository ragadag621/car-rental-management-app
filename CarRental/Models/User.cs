using System.ComponentModel.DataAnnotations;

namespace CarRental.Models;

/// <summary>
/// A registered application user. Passwords are stored hashed (never in plain text).
/// </summary>
public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    /// <summary>Salted hash of the user's password.</summary>
    public string PasswordHash { get; set; } = string.Empty;
}

/// <summary>Form model for the registration page.</summary>
public class RegisterModel
{
    [Required(ErrorMessage = "Please enter your full name.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>Form model for the login page.</summary>
public class LoginModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}

/// <summary>Outcome of an authentication attempt.</summary>
public class AuthResult
{
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
    public User? User { get; init; }

    public static AuthResult Fail(string message) => new() { Success = false, ErrorMessage = message };
    public static AuthResult Ok(User user) => new() { Success = true, User = user };
}
