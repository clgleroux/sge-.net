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
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {employeeDepartment.EmployeeId}"
                );
            }

            var departmentGet = await _departmentRepository.GetDepartmentByIdAsync(
                employeeDepartment.DepartmentId
            );
            if (departmentGet is null)
            {
                throw new Exception(
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {employeeDepartment.DepartmentId}"
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

        //public async Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId)
        //{
        //    var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

        //    if (employee is null)
        //        throw new Exception(
        //            $"Echec de recupération des informations d'un département car il n'existe pas : {employeeId}"
        //        );

        //    return new ReadEmployee() { Id = employee.EmployeeId, Email = employee.Email, };
        //}

        public async Task<ReadEmployeeDepartment> DeleteEmployeeDepartmentById(
            CreateEmployeeDepartment employeeDepartment
        )
        {
            var employeeGet = await _employeeRepository.GetEmployeeByIdAsync(
                employeeDepartment.EmployeeId
            );
            if (employeeGet is not null)
            {
                throw new Exception(
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {employeeDepartment.EmployeeId}"
                );
            }

            var departmentGet = await _departmentRepository.GetDepartmentByIdAsync(
                employeeDepartment.DepartmentId
            );
            if (departmentGet is not null)
            {
                throw new Exception(
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {employeeDepartment.DepartmentId}"
                );
            }

            await _employeeRepository.RemoveEmployeeDepartmentAsync(employeeGet, departmentGet);

            await _departmentRepository.RemoveDepartmentEmployeeAsync(departmentGet, employeeGet);

            return new ReadEmployeeDepartment()
            {
                EmployeeId = employeeDepartment.EmployeeId,
                DepartmentId = employeeDepartment.DepartmentId,
            };
        }
    }
}
