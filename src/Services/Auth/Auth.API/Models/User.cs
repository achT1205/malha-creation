using Microsoft.AspNetCore.Identity;

namespace Auth.API.Models;

public class User : IdentityUser
{
    public string? Initials { get; set; }
}
