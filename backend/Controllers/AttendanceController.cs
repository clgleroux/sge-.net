using backend.Dto.Attendance;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly AttendanceService _attendanceService;

        public AttendancesController(AttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // POST api/<AttendancesController>
        [HttpPost]
        public async Task<ActionResult<ReadAttendance>> Post([FromBody] CreateAttendance attendance)
        {
            if (
                attendance == null
                || attendance.EmployeeId == null
                || attendance.EndDate == null
                || attendance.StartDate == null
            )
            {
                return BadRequest(
                    "Echec de création d'un departement : les informations sont null ou vides"
                );
            }

            try
            {
                var attendanceCreated = await _attendanceService.CreateAttendanceAsync(attendance);
                return Ok(attendanceCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<AttendancesController>
        [HttpGet]
        public async Task<ActionResult<List<ReadAttendance>>> Get()
        {
            try
            {
                var attendances = await _attendanceService.GetAttendances();
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<AttendancesController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAttendance>> Get(int id)
        {
            try
            {
                var attendance = await _attendanceService.GetAttendanceByIdAsync(id);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<AttendancesController>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadAttendance>> Put(
            int id,
            [FromBody] UpdateAttendance updateAttendance
        )
        {
            try
            {
                var attendance = await _attendanceService.UpdateAttendanceAsync(
                    id,
                    updateAttendance
                );
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<AttendancesController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadAttendance>> Delete(int id)
        {
            try
            {
                var attendance = await _attendanceService.DeleteAttendanceById(id);
                return Ok(attendance);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
