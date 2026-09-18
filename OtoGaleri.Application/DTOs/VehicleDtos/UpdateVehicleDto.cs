namespace OtoGaleri.Application.DTOs.VehicleDtos;

public class UpdateVehicleDto
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int Year { get; set; }
    public string Color { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Kilometer { get; set; }
}