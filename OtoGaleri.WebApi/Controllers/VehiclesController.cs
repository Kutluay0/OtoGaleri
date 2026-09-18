using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Application.DTOs.VehicleDtos;
using OtoGaleri.Application.Interfaces;

namespace OtoGaleri.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        if (vehicle == null)
            return NotFound("Araç bulunamadı.");

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateVehicleDto createVehicleDto)
    {
        await _vehicleService.AddVehicleAsync(createVehicleDto);
        return Ok("Araç başarıyla eklendi.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateVehicleDto updateVehicleDto)
    {
        await _vehicleService.UpdateVehicleAsync(updateVehicleDto);
        return Ok("Araç bilgileri başarıyla güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _vehicleService.DeleteVehicleAsync(id);
        return Ok("Araç başarıyla silindi.");
    }
}