using AutoMapper;
using OtoGaleri.Application.DTOs.VehicleDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IGenericRepository<Vehicle> _vehicleRepository;
    private readonly IMapper _mapper;

    public VehicleService(IGenericRepository<Vehicle> vehicleRepository, IMapper mapper)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<VehicleDto?> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        return vehicle == null ? null : _mapper.Map<VehicleDto>(vehicle);
    }

    public async Task AddVehicleAsync(CreateVehicleDto vehicleDto)
    {
        var vehicle = _mapper.Map<Vehicle>(vehicleDto);

        // Oluşturulma tarihini sistem otomatik verir
        vehicle.CreatedDate = DateTime.Now;

        await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.SaveChangesAsync();
    }

    public async Task UpdateVehicleAsync(UpdateVehicleDto vehicleDto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleDto.Id);
        if (vehicle != null)
        {
            _mapper.Map(vehicleDto, vehicle);
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle != null)
        {
            vehicle.IsDeleted = true;
            _vehicleRepository.Update(vehicle);
            await _vehicleRepository.SaveChangesAsync();
        }
    }
}