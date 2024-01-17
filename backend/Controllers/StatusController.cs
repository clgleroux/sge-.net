using backend.Dto.Status;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly StatusService _statusService;

        public StatusController(StatusService statusService)
        {
            _statusService = statusService;
        }

        // GET api/<StatusController>
        [HttpGet]
        public async Task<ActionResult<List<ReadStatus>>> Get()
        {
            try
            {
                var statuss = await _statusService.GetStatuses();
                return Ok(statuss);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<StatusController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadStatus>> Get(int id)
        {
            try
            {
                var status = await _statusService.GetStatusByIdAsync(id);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
