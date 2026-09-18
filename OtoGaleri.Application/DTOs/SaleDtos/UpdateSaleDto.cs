using OtoGaleri.Core.Enums;

namespace OtoGaleri.Application.DTOs.SaleDtos;

public class UpdateSaleDto
{
    public int Id { get; set; }
    public decimal SalePrice { get; set; }
    public PaymentType PaymentType { get; set; }
}