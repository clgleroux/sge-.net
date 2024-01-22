using backend.Dto.EmployeeDepartment;
using backend.Entities;
using backend.Infrastructures.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class EmployeeRepository
    {
        private readonly ManageEmployeeDbContext _manageEmployeeDbContext;

        public EmployeeRepository(ManageEmployeeDbContext manageEmployeeDbContext)
        {
            _manageEmployeeDbContext = manageEmployeeDbContext;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee departmentToCreate)
        {
            await _manageEmployeeDbContext.Employees.AddAsync(departmentToCreate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return departmentToCreate;
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string departmentName)
        {
            return await _manageEmployeeDbContext.Employees.FirstOrDefaultAsync(
                x => x.Email == departmentName
            );
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _manageEmployeeDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _manageEmployeeDbContext.Employees.FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId
            );
        }

        public async Task<Employee> GetEmployeeByIdWithIncludeAsync(int employeeId)
        {
            return await _manageEmployeeDbContext
                .Employees.Include(x => x.Departments)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _manageEmployeeDbContext.Employees.Update(employeeToUpdate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return employeeToUpdate;
        }

        public async Task<Employee> DeleteEmployeeByIdAsync(int employeeId)
        {
            var employeeToDelete = await _manageEmployeeDbContext.Employees.FindAsync(employeeId);

            var departmentsToRemove = employeeToDelete.Departments.ToList();
            foreach (var department in departmentsToRemove)
            {
                employeeToDelete.Departments.Remove(department);
            }

            var leaverequest = await _manageEmployeeDbContext.Leaverequests.FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId
            );

            var attendances = await _manageEmployeeDbContext.Attendances.FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId
            );

            if (leaverequest is not null || attendances is not null)
            {
                throw new Exception(
                    "Echec de suppression car ce employee est lié à des departments"
                );
            }

            await _manageEmployeeDbContext.SaveChangesAsync();

            _manageEmployeeDbContext.Employees.Remove(employeeToDelete);
            await _manageEmployeeDbContext.SaveChangesAsync();
            return employeeToDelete;
        }

        public async Task<Employee> AddEmployeeDepartmentAsync(
            Employee employee,
            Department department
        )
        {
            employee.Departments.Add(department);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> RemoveEmployeeDepartmentAsync(
            Employee employee,
            Department department
        )
        {
            employee.Departments.Remove(department);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return employee;
        }
    }
}
