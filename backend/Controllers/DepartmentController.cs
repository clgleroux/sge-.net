using backend.Dto.Department;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService _departementService;

        public DepartmentsController(DepartmentService departementService)
        {
            _departementService = departementService;
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<ActionResult<ReadDepartment>> Post([FromBody] CreateDepartment department)
        {
            if (
                department == null
                || string.IsNullOrWhiteSpace(department.Name)
                || string.IsNullOrWhiteSpace(department.Address)
                || string.IsNullOrWhiteSpace(department.Description)
            )
            {
                return BadRequest(
                    "Echec de création d'un departement : les informations sont null ou vides"
                );
            }

            try
            {
                var departmentCreated = await _departementService.CreateDepartmentAsync(department);
                return Ok(departmentCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<DepartmentsController>
        [HttpGet]
        public async Task<ActionResult<ReadDepartment[]>> Get()
        {
            try
            {
                var departments = await _departementService.GetDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<DepartmentsController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadDepartment[]>> Get(int id)
        {
            try
            {
                var department = await _departementService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
