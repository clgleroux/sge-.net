using backend.Dto.Department;
using backend.Entities;
using backend.Infrastructures.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class DepartmentRepository
    {
        private readonly ManageEmployeeDbContext _manageEmployeeDbContext;

        public DepartmentRepository(ManageEmployeeDbContext manageEmployeeDbContext)
        {
            _manageEmployeeDbContext = manageEmployeeDbContext;
        }

        public async Task<Department> CreateDepartmentAsync(Department departmentToCreate)
        {
            await _manageEmployeeDbContext.Departments.AddAsync(departmentToCreate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return departmentToCreate;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string departmentName)
        {
            return await _manageEmployeeDbContext.Departments.FirstOrDefaultAsync(
                x => x.Name == departmentName
            );
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _manageEmployeeDbContext.Departments.ToListAsync();
        }

        public async Task<List<Department>> GetDepartmentsWithIncludeAsync()
        {
            return await _manageEmployeeDbContext
                .Departments.Include(x => x.Employees)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            return await _manageEmployeeDbContext.Departments.FirstOrDefaultAsync(
                x => x.DepartmentId == departmentId
            );
        }

        public async Task<Department> GetDepartmentByIdWithIncludeAsync(int departmentId)
        {
            return await _manageEmployeeDbContext
                .Departments.Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
        }

        public async Task<Department> UpdateDepartmentAsync(Department departmentToUpdate)
        {
            _manageEmployeeDbContext.Departments.Update(departmentToUpdate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return departmentToUpdate;
        }

        public async Task<Department> DeleteDepartmentByIdAsync(int departmentId)
        {
            var departmentToDelete = await _manageEmployeeDbContext.Departments.FindAsync(
                departmentId
            );
            _manageEmployeeDbContext.Departments.Remove(departmentToDelete);
            await _manageEmployeeDbContext.SaveChangesAsync();
            return departmentToDelete;
        }

        internal Task<Department> UpdateDepartmentAsync(UpdateDepartment department)
        {
            throw new NotImplementedException();
        }

        public async Task<Department> AddDepartmentEmployeeAsync(
            Department department,
            Employee employee
        )
        {
            department.Employees.Add(employee);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return department;
        }

        public async Task<Department> RemoveDepartmentEmployeeAsync(
            Department department,
            Employee employee
        )
        {
            department.Employees.Remove(employee);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return department;
        }
    }
}
