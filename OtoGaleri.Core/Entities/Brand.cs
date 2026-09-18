namespace OtoGaleri.Core.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<Model> Models { get; set; } = new List<Model>();
}