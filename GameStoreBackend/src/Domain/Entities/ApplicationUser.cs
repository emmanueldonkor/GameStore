namespace Domain.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public Role Role { get; set; }
    public IReadOnlyList<Order>? Orders { get; set; }
}

public enum Role
{
    User = 0,
    Admin = 1
}




