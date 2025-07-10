using Microsoft.AspNetCore.Identity;

namespace ddla.ITApplication.Database.Models.DomainModels.Account;

public class ddlaUser : IdentityUser
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string? ProfilePictureUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
