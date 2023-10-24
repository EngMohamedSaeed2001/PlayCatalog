using PlayCatalog.DTOS;
using PlayCatalog.Entity;

namespace PlayCatalog
{
    public static class Extensions
    {
        public static ItemDTO AsDTO(this Item item) { 
            return new ItemDTO(item.Id,item.Name,item.Description,item.Price,item.CreatedDate);
        }
    }
}
