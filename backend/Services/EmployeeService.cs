using backend.Dto.Employee;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ReadEmployee> CreateEmployeeAsync(CreateEmployee employee)
        {
            var employeeGet = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGet is not null)
            {
                throw new Exception(
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {employee.Email}"
                );
            }

            var employeeTocreate = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
            };

            var employeeCreated = await _employeeRepository.CreateEmployeeAsync(employeeTocreate);

            return new ReadEmployee()
            {
                Id = employeeCreated.EmployeeId,
                Email = employeeCreated.Email,
            };
        }

        public async Task<List<ReadEmployee>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();

            List<ReadEmployee> readEmployees = new List<ReadEmployee>();

            foreach (var employee in employees)
            {
                readEmployees.Add(
                    new ReadEmployee()
                    {
                        Id = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        PhoneNumber = employee.PhoneNumber,
                        Position = employee.Position,
                    }
                );
            }

            return readEmployees;
        }

        public async Task<ReadEmployee> GetEmployeeByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);

            if (employee is null)
                throw new Exception(
                    $"Echec de recupération des informations d'un département car il n'existe pas : {employeeId}"
                );

            return new ReadEmployee() { Id = employee.EmployeeId, Email = employee.Email, };
        }

        public async Task<Employee> UpdateEmployeeAsync(int employeeId, UpdateEmployee employee)
        {
            var employeeUpdate =
                await _employeeRepository.GetEmployeeByIdAsync(employeeId)
                ?? throw new Exception(
                    $"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {employeeId}"
                );

            var employeeGet = await _employeeRepository.GetEmployeeByEmailAsync(employee.Email);
            if (employeeGet is not null && employeeId != employeeGet.EmployeeId)
            {
                throw new Exception(
                    $"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {employee.Email}"
                );
            }

            employeeUpdate.FirstName = employee.FirstName;
            employeeUpdate.LastName = employee.LastName;
            employeeUpdate.Email = employee.Email;
            employeeUpdate.PhoneNumber = employee.PhoneNumber;
            employeeUpdate.Position = employee.Position;

            return await _employeeRepository.UpdateEmployeeAsync(employeeUpdate);
        }

        public async Task<Employee> DeleteEmployeeById(int employeeId)
        {
            var employeeGet =
                await _employeeRepository.GetEmployeeByIdWithIncludeAsync(employeeId)
                ?? throw new Exception(
                    $"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {employeeId}"
                );

            if (employeeGet.Departments.Any())
            {
                throw new Exception(
                    "Echec de suppression car ce departement est lié à des employés"
                );
            }

            return await _employeeRepository.DeleteEmployeeByIdAsync(employeeId);
        }
    }
}
