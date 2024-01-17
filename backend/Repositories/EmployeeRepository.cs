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

        public async Task<Employee> GetDepartmentByIdWithIncludeAsync(int employeeId)
        {
            return await _manageEmployeeDbContext
                .Employees.Include(x => x.Departments)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task UpdateEmployeeAsync(Employee employeeToUpdate)
        {
            _manageEmployeeDbContext.Employees.Update(employeeToUpdate);
            await _manageEmployeeDbContext.SaveChangesAsync();
        }

        public async Task<Employee> DeleteEmployeeByIdAsync(int employeeId)
        {
            var employeeToDelete = await _manageEmployeeDbContext.Employees.FindAsync(employeeId);
            _manageEmployeeDbContext.Employees.Remove(employeeToDelete);
            await _manageEmployeeDbContext.SaveChangesAsync();
            return employeeToDelete;
        }
    }
}
