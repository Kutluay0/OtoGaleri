using OtoGaleri.Core.Enums;

namespace OtoGaleri.Core.Entities;

public class Sale : BaseEntity
{
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; } // Satışı yapan danışman
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public decimal SalePrice { get; set; }
    public PaymentType PaymentType { get; set; }

    // Navigation Properties
    public Vehicle Vehicle { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public User User { get; set; } = null!;
}