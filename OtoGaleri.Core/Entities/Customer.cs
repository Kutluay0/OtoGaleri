namespace OtoGaleri.Core.Entities;

public class Customer : BaseEntity
{
    public int UserId { get; set; } // Müşteri ile ilgilenen danışman
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Navigation Properties
    public User User { get; set; } = null!;
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}