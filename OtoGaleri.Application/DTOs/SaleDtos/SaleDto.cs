using OtoGaleri.Core.Enums;

namespace OtoGaleri.Application.DTOs.SaleDtos;

public class SaleDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public int UserId { get; set; }
    public decimal SalePrice { get; set; }
    public DateTime SaleDate { get; set; }
    public PaymentType PaymentType { get; set; }
}