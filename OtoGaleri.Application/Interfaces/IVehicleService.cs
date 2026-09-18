using OtoGaleri.Application.DTOs.VehicleDtos;
using static System.Net.Mime.MediaTypeNames;


namespace OtoGaleri.Application.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
    Task<VehicleDto?> GetVehicleByIdAsync(int id);
    Task AddVehicleAsync(CreateVehicleDto vehicleDto);
    Task UpdateVehicleAsync(UpdateVehicleDto vehicleDto);
    Task DeleteVehicleAsync(int id);
}