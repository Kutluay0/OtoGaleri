using OtoGaleri.Core.Enums;

namespace OtoGaleri.Core.Entities;

public class Model : BaseEntity
{
    public int BrandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public BodyType BodyType { get; set; }

    // Navigation Properties
    public Brand Brand { get; set; } = null!;
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}