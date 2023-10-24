using System.ComponentModel.DataAnnotations;

namespace PlayCatalog.DTOS
{
    public record ItemDTO(Guid id, string name, string description, decimal price, DateTimeOffset createdDate);
    public record CreateItemDTO([Required] string name, string description,[Range(1,100000)] decimal price);
    public record UpdateItemDTO([Required] string name, string description, [Range(1, 100000)] decimal price);
}
