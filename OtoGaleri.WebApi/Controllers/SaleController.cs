using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Application.DTOs.SaleDtos;
using OtoGaleri.Application.Interfaces;

namespace OtoGaleri.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SalesController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sales = await _saleService.GetAllSalesAsync();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sale = await _saleService.GetSaleByIdAsync(id);
        if (sale == null)
            return NotFound("Satış kaydı bulunamadı.");

        return Ok(sale);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSaleDto createSaleDto)
    {
        await _saleService.AddSaleAsync(createSaleDto);
        return Ok("Satış işlemi başarıyla gerçekleştirildi.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSaleDto updateSaleDto)
    {
        await _saleService.UpdateSaleAsync(updateSaleDto);
        return Ok("Satış bilgileri güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _saleService.DeleteSaleAsync(id);
        return Ok("Satış kaydı silindi.");
    }
}