using CheatSheet.Application.IRepos;
using Microsoft.AspNetCore.Mvc;

namespace CheatSheet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/flowers/")]
    public class FlowerController : Controller
    {
        private readonly ILogger<FlowerController> _logger;
        private IFlowerRepo _repo;

        public FlowerController(ILogger<FlowerController> logger, IFlowerRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }


        [HttpPost]
        public async Task<ActionResult<FlowerDTO>> Post([FromQuery] FlowerDTO flowerDTO)
        {
            try
            {
                await _repo.AddAsync(flowerDTO);
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
                FlowerDTO? found = _repo.GetByID(id);
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
        public async Task<ActionResult<FlowerDTO>> UpdateGarden([FromForm] FlowerDTO flowerDTO)
        {
            try
            {
                FlowerDTO? found = _repo.GetByID(flowerDTO.ID);
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
        public ActionResult<IEnumerable<FlowerDTO>> GetGardens()
        {
            return Ok(_repo.GetAll());
        }


        [HttpGet("getbyid/{id}")]
        public ActionResult<IEnumerable<FlowerDTO>> GetByID(Guid id)
        {
            return Ok(_repo.GetByID(id));
        }
    }
}
