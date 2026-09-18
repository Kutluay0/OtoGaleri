using OtoGaleri.Application.DTOs.SaleDtos;

namespace OtoGaleri.Application.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<SaleDto>> GetAllSalesAsync();
    Task<SaleDto?> GetSaleByIdAsync(int id);
    Task AddSaleAsync(CreateSaleDto saleDto);
    Task UpdateSaleAsync(UpdateSaleDto saleDto);
    Task DeleteSaleAsync(int id);
}