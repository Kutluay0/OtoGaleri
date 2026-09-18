namespace OtoGaleri.Application.DTOs.ModelDtos;

public class UpdateModelDto
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; } = string.Empty;
}