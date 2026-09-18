using OtoGaleri.Core.Enums;

namespace OtoGaleri.Application.DTOs.VehicleDtos;

public class CreateVehicleDto
{
    public int ModelId { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int Kilometer { get; set; }
    public string Color { get; set; } = string.Empty;
    public FuelType FuelType { get; set; }
    public TransmissionType TransmissionType { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.Stokta; // Varsayılan: Satışta
}