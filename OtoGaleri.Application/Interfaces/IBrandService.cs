using OtoGaleri.Application.DTOs.BrandDtos;

namespace OtoGaleri.Application.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<BrandDto?> GetBrandByIdAsync(int id);
    Task AddBrandAsync(CreateBrandDto brandDto);
    Task UpdateBrandAsync(UpdateBrandDto brandDto); 
    Task DeleteBrandAsync(int id);
}