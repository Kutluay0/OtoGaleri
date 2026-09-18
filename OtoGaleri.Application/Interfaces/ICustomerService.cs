using OtoGaleri.Application.DTOs.CustomerDtos;

namespace OtoGaleri.Application.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
    Task<CustomerDto?> GetCustomerByIdAsync(int id);
    Task AddCustomerAsync(CreateCustomerDto customerDto);
    Task UpdateCustomerAsync(UpdateCustomerDto customerDto);
    Task DeleteCustomerAsync(int id);
}