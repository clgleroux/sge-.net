using backend.Dto.Employee;
using backend.Entities;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<ActionResult<ReadEmployee>> Post([FromBody] CreateEmployee employee)
        {
            if (
                employee == null
                || string.IsNullOrWhiteSpace(employee.FirstName)
                || string.IsNullOrWhiteSpace(employee.LastName)
                || string.IsNullOrWhiteSpace(employee.Email)
                || !IsValidEmail(employee.Email)
                || string.IsNullOrWhiteSpace(employee.PhoneNumber)
                || employee.Position == null
            )
            {
                return BadRequest(
                    "Echec de création d'un employee : les informations sont null ou vides ou incorrect"
                );
            }

            try
            {
                var employeeCreated = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(employeeCreated);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<EmployeesController>
        [HttpGet]
        public async Task<ActionResult<List<ReadEmployee>>> Get()
        {
            try
            {
                var employees = await _employeeService.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET api/<EmployeesController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployee>> Get(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/<EmployeesController>/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ReadEmployee>> Put(
            int id,
            [FromBody] UpdateEmployee updateEmployee
        )
        {
            if (
                updateEmployee == null
                || string.IsNullOrWhiteSpace(updateEmployee.FirstName)
                || string.IsNullOrWhiteSpace(updateEmployee.LastName)
                || string.IsNullOrWhiteSpace(updateEmployee.Email)
                || !IsValidEmail(updateEmployee.Email)
                || string.IsNullOrWhiteSpace(updateEmployee.PhoneNumber)
                || updateEmployee.Position == null
            )
            {
                return BadRequest(
                    "Echec de création d'un employee : les informations sont null ou vides ou incorrect"
                );
            }

            try
            {
                var employee = await _employeeService.UpdateEmployeeAsync(id, updateEmployee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/<EmployeesController>/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReadEmployee>> Delete(int id)
        {
            try
            {
                var employee = await _employeeService.DeleteEmployeeById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
