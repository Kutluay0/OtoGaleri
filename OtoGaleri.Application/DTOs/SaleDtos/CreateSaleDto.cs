using OtoGaleri.Core.Enums;

namespace OtoGaleri.Application.DTOs.SaleDtos;

public class CreateSaleDto
{
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public decimal SalePrice { get; set; }
    public PaymentType PaymentType { get; set; }
}