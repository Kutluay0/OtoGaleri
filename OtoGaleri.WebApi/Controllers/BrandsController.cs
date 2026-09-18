using Microsoft.AspNetCore.Authorization; // <-- 1. Bu using'i ekleyin
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Application.DTOs.BrandDtos;
using OtoGaleri.Application.Interfaces;

namespace OtoGaleri.WebApi.Controllers;

[Authorize] // <-- 2. Bu özniteliği ekleyin (Artık tüm metotlar Token ister)
[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandsController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _brandService.GetAllBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brand = await _brandService.GetBrandByIdAsync(id);
        if (brand == null)
            return NotFound("Marka bulunamadı.");

        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandDto createBrandDto)
    {
        await _brandService.AddBrandAsync(createBrandDto);
        return Ok("Marka başarıyla eklendi.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBrandDto updateBrandDto)
    {
        await _brandService.UpdateBrandAsync(updateBrandDto);
        return Ok("Marka başarıyla güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _brandService.DeleteBrandAsync(id);
        return Ok("Marka başarıyla silindi.");
    }
}