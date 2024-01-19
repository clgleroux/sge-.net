using backend.Dto.Employee;
using backend.Dto.EmployeeDepartment;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
  public class EmployeeDepartmentService
  {
    private readonly EmployeeRepository _employeeRepository;
    private readonly DepartmentRepository _departmentRepository;

    public EmployeeDepartmentService(
        EmployeeRepository employeeRepository,
        DepartmentRepository departmentRepository
    )
    {
      _employeeRepository = employeeRepository;
      _departmentRepository = departmentRepository;
    }

    public async Task<ReadEmployeeDepartment> CreateEmployeeDepartmentAsync(
        CreateEmployeeDepartment employeeDepartment
    )
    {
      var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(
          employeeDepartment.EmployeeId
      );
      if (employeeGet is null)
      {
        throw new Exception(
            $"Echec de création d'un département : Il n'existe pas un employee avec cet id {employeeDepartment.EmployeeId}"
        );
      }

      var departmentGet = await _departmentRepository.GetDepartmentByIdAsync(
          employeeDepartment.DepartmentId
      );
      if (departmentGet is null)
      {
        throw new Exception(
            $"Echec de création d'un département : Il n'existe pas un department avec cet id {employeeDepartment.DepartmentId}"
        );
      }

      await _employeeRepository.AddEmployeeDepartmentAsync(employeeGet, departmentGet);

      await _departmentRepository.AddDepartmentEmployeeAsync(departmentGet, employeeGet);

      return new ReadEmployeeDepartment()
      {
        EmployeeId = employeeDepartment.EmployeeId,
        DepartmentId = employeeDepartment.DepartmentId,
      };
    }

    public async Task<List<ReadEmployeeDepartment>> GetEmployeeDepartments()
    {
      var departments = await _departmentRepository.GetDepartmentsWithIncludeAsync();

      List<ReadEmployeeDepartment> readEmployeeDepartment =
          new List<ReadEmployeeDepartment>();

      foreach (var department in departments)
      {
        foreach (var employee in department.Employees)
        {
          readEmployeeDepartment.Add(
              new ReadEmployeeDepartment()
              {
                EmployeeId = employee.EmployeeId,
                DepartmentId = department.DepartmentId,
                EmployeeEmail = employee.Email,
                DepartmentName = department.Name
              }
          );
        }
      }

      return readEmployeeDepartment;
    }

    public async Task<ReadEmployeeDepartment> DeleteEmployeeDepartmentById(
        int employeeId, int departmentId

    )
    {
      var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(
          employeeId
      );
      if (employeeGet is not null)
      {
        throw new Exception(
            $"Echec de création d'un employee département : Il n'existe pas un employee avec cet id {employeeId}"
        );
      }

      var departmentGet = await _departmentRepository.GetDepartmentByIdAsync(
          departmentId
      );
      if (departmentGet is not null)
      {
        throw new Exception(
            $"Echec de création d'un employee département : Il n'existe pas un department avec cet id {departmentId}"
        );
      }

      await _employeeRepository.RemoveEmployeeDepartmentAsync(employeeGet, departmentGet);

      await _departmentRepository.RemoveDepartmentEmployeeAsync(departmentGet, employeeGet);

      return new ReadEmployeeDepartment()
      {
        EmployeeId = employeeId,
        DepartmentId = departmentId,
      };
    }
  }
}
