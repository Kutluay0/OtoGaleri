namespace OtoGaleri.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public int? CreatedUserId { get; set; } // int? (nullable) yapıldı
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public int? UpdateUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}