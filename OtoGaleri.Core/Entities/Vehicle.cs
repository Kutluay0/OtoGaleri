using OtoGaleri.Core.Enums;

namespace OtoGaleri.Core.Entities;

public class Vehicle : BaseEntity
{
    public int ModelId { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int Kilometer { get; set; }
    public string Color { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.Stokta;

    // Navigation Properties
    public Model Model { get; set; } = null!;
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
