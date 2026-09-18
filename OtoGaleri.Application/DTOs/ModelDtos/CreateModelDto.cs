using OtoGaleri.Core.Enums;

namespace OtoGaleri.Application.DTOs.ModelDtos;

public class CreateModelDto
{
    public int BrandId { get; set; }
    public string Name { get; set; } = string.Empty;
    public BodyType BodyType { get; set; }
}