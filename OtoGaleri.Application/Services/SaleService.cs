using System;
using AutoMapper;
using OtoGaleri.Application.DTOs.SaleDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Enums;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class SaleService : ISaleService
{
    private readonly IGenericRepository<Sale> _saleRepository;
    private readonly IGenericRepository<Vehicle> _vehicleRepository;
    private readonly IMapper _mapper;

    public SaleService(
        IGenericRepository<Sale> saleRepository,
        IGenericRepository<Vehicle> vehicleRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
    {
        var sales = await _saleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<SaleDto?> GetSaleByIdAsync(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        return sale == null ? null : _mapper.Map<SaleDto>(sale);
    }

    public async Task AddSaleAsync(CreateSaleDto saleDto)
    {
        var sale = _mapper.Map<Sale>(saleDto);
        sale.SaleDate = DateTime.Now;

        await _saleRepository.AddAsync(sale);

        var vehicle = await _vehicleRepository.GetByIdAsync(saleDto.VehicleId);
        if (vehicle != null)
        {
            vehicle.Status = VehicleStatus.Satildi;
            _vehicleRepository.Update(vehicle);
        }

        await _saleRepository.SaveChangesAsync();
    }

    public async Task UpdateSaleAsync(UpdateSaleDto saleDto)
    {
        var sale = await _saleRepository.GetByIdAsync(saleDto.Id);
        if (sale != null)
        {
            _mapper.Map(saleDto, sale);
            _saleRepository.Update(sale);
            await _saleRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteSaleAsync(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        if (sale != null)
        {
            sale.IsDeleted = true;
            _saleRepository.Update(sale);
            await _saleRepository.SaveChangesAsync();
        }
    }
}