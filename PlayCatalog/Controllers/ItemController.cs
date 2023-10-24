using Microsoft.AspNetCore.Mvc;
using PlayCatalog.DTOS;
using PlayCatalog.Entity;
using PlayCatalog.Reository;

namespace PlayCatalog.Controllers
{
    [ApiController]
    
    [Route("items")]

    public class ItemController : ControllerBase
    {
        private static readonly ItemRepo itemRepo = new ();
        

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetAllAsync() {  
        var items = (await itemRepo.GetAllAsync()).Select(item =>item.AsDTO());
            
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetByIdAsync(Guid id) {
            var item = await itemRepo.GetAsync(id);

            if (item == null){
                return NotFound();
            }

            return item.AsDTO();
        }

        [HttpPost]

        public async Task<ActionResult<ItemDTO>> CreateAsync(CreateItemDTO itemDTO)
        {

            var item = new Item
            {
               
               Name= itemDTO.name,
              Description=  itemDTO.description,
               Price = itemDTO.price,
               CreatedDate = DateTimeOffset.UtcNow };
            
            await itemRepo.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync),new {id =item.Id},item);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(Guid id ,UpdateItemDTO itemDTO)
        {
            var item = await itemRepo.GetAsync(id);

            if(item == null){ 
                return NotFound();
            }


            item.Name = itemDTO.name;
            item.Description = itemDTO.description;
            item.Price = itemDTO.price;
           
            await itemRepo.UpdateAsync(item);
            

            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(Guid id) {
            var item = await itemRepo.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemRepo.RemoveAsync(id);

            return NoContent(); 
        }
    }
}
