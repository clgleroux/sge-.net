using backend.Dto.Leaverequest;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaverequestsController : ControllerBase
    {
        private readonly LeaverequestService _leaverequestService;

        public LeaverequestsController(LeaverequestService leaverequestService)
        {
            _leaverequestService = leaverequestService;
        }

        // POST api/<LeaverequestsController>
        [HttpPost]
        public async Task<ActionResult<ReadLeaverequest>> Post(
            [FromBody] CreateLeaverequest leaverequest
        )
        {
            if (
                leaverequest == null
                || leaverequest.EmployeeId == null
                || leaverequest.StatusId == null
                || leaverequest.EndDate == null
                || leaverequest.StartDate == null
                || leaverequest.RequestDate == null
            )
            {
                return BadRequest(
                    "Echec de création d'un departement : les informations sont null ou vides"
                );
            }

            try
            {
                var leaverequestCreated = await _leaverequestService.CreateLeaverequestAsync(
                    leaverequest
                );
                return Ok(leaverequestCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<LeaverequestsController>
        [HttpGet]
        public async Task<ActionResult<List<ReadLeaverequest>>> Get()
        {
            try
            {
                var leaverequests = await _leaverequestService.GetLeaverequests();
                return Ok(leaverequests);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<LeaverequestsController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadLeaverequest>> Get(int id)
        {
            try
            {
                var leaverequest = await _leaverequestService.GetLeaverequestByIdAsync(id);
                return Ok(leaverequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<LeaverequestsController>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadLeaverequest>> Put(
            int id,
            [FromBody] UpdateLeaverequest updateLeaverequest
        )
        {
            try
            {
                var leaverequest = await _leaverequestService.UpdateLeaverequestAsync(
                    id,
                    updateLeaverequest
                );
                return Ok(leaverequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<LeaverequestsController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadLeaverequest>> Delete(int id)
        {
            try
            {
                var leaverequest = await _leaverequestService.DeleteLeaverequestById(id);
                return Ok(leaverequest);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
