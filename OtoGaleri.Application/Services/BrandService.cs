using AutoMapper;
using OtoGaleri.Application.DTOs.BrandDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class BrandService : IBrandService
{
    private readonly IGenericRepository<Brand> _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IGenericRepository<Brand> brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto?> GetBrandByIdAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        return brand == null ? null : _mapper.Map<BrandDto>(brand);
    }

    public async Task AddBrandAsync(CreateBrandDto brandDto)
    {
        var brand = _mapper.Map<Brand>(brandDto);
        await _brandRepository.AddAsync(brand);
        await _brandRepository.SaveChangesAsync();
    }

    public async Task UpdateBrandAsync(UpdateBrandDto brandDto)
    {
        var brand = await _brandRepository.GetByIdAsync(brandDto.Id);
        if (brand != null)
        {
            _mapper.Map(brandDto, brand);
            _brandRepository.Update(brand);
            await _brandRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteBrandAsync(int id)
    {
        var brand = await _brandRepository.GetByIdAsync(id);
        if (brand != null)
        {
            // Soft Delete mantığı
            brand.IsDeleted = true;
            _brandRepository.Update(brand);
            await _brandRepository.SaveChangesAsync();
        }
    }
}