using AutoMapper;
using OtoGaleri.Application.DTOs.CustomerDtos;
using OtoGaleri.Application.Interfaces;
using OtoGaleri.Core.Entities;
using OtoGaleri.Core.Interfaces;

namespace OtoGaleri.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly IGenericRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IGenericRepository<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDto>>(customers);
    }

    public async Task<CustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        return customer == null ? null : _mapper.Map<CustomerDto>(customer);
    }

    public async Task AddCustomerAsync(CreateCustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _customerRepository.AddAsync(customer);
        await _customerRepository.SaveChangesAsync();
    }

    public async Task UpdateCustomerAsync(UpdateCustomerDto customerDto)
    {
        var customer = await _customerRepository.GetByIdAsync(customerDto.Id);
        if (customer != null)
        {
            _mapper.Map(customerDto, customer);
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetByIdAsync(id);
        if (customer != null)
        {
            customer.IsDeleted = true;
            _customerRepository.Update(customer);
            await _customerRepository.SaveChangesAsync();
        }
    }
}