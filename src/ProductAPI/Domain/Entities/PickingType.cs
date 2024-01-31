namespace ProductAPI.Domain.Entities;

public class PickingType : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Barcode { get; set; }
    public string? SequenceCode { get; set; }
}
