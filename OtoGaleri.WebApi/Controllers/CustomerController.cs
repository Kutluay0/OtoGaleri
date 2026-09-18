using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Application.DTOs.CustomerDtos;
using OtoGaleri.Application.Interfaces;

namespace OtoGaleri.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound("Müşteri bulunamadı.");

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto createCustomerDto)
    {
        await _customerService.AddCustomerAsync(createCustomerDto);
        return Ok("Müşteri başarıyla eklendi.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerDto updateCustomerDto)
    {
        await _customerService.UpdateCustomerAsync(updateCustomerDto);
        return Ok("Müşteri bilgileri başarıyla güncellendi.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return Ok("Müşteri başarıyla silindi.");
    }
}