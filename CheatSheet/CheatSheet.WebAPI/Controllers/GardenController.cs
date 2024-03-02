using CheatSheet.Application.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace CheatSheet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/gardens/")]
    public class GardenController : Controller
    {
        private readonly ILogger<GardenController> _logger;
        private IGardenRepo _repo;

        public GardenController(ILogger<GardenController> logger, IGardenRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }


        [HttpPost]
        public async Task<ActionResult<GardenDTO>> Post([FromQuery] GardenDTO gardenDTO)
        {
            try
            {
                await _repo.AddAsync(gardenDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveGarden(Guid id)
        {
            try
            {
                GardenDTO? found = _repo.GetByID(id);
                if (found == null) { return NotFound(); }
                await _repo.RemoveAsync(found);
                return Ok(found);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch]
        public async Task<ActionResult<GardenDTO>> UpdateGarden([FromForm] GardenDTO gardenDTO)
        {
            try
            {
                GardenDTO? found = _repo.GetByID(gardenDTO.ID);
                if (found == null) { return NotFound(); }
                await _repo.UpdateAsync(found);
                return Ok(found);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getall")]
        public ActionResult<IEnumerable<GardenDTO>> GetGardens()
        {
            return Ok(_repo.GetAll());
        }


        [HttpGet("getbyid/{id}")]
        public ActionResult<IEnumerable<GardenDTO>> GetByID(Guid id)
        {
            return Ok(_repo.GetByID(id));
        }


        [HttpGet("getbyowner/{id}")]
        public async Task<ActionResult<IEnumerable<GardenDTO>>> GetByOwner(Guid id)
        {
            return Ok(await _repo.GetByOwnerAsync(id));
        }


        [HttpGet("getbyflower")]
        public ActionResult<IEnumerable<GardenDTO>> GetByFlower([FromBody] FlowerDTO flowerDTO)
        {
            return Ok(_repo.GetByFlower(flowerDTO));
        }


        [HttpGet("getbytree")]
        public ActionResult<IEnumerable<GardenDTO>> GetByTree([FromBody] TreeDTO treeDTO)
        {
            return Ok(_repo.GetByTree(treeDTO));
        }
    }
}
