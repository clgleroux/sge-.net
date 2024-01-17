using backend.Dto.Department;
using backend.Entities;
using backend.Repositories;

namespace backend.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ReadDepartment> CreateDepartmentAsync(CreateDepartment department)
        {
            var departmentGet = await _departmentRepository.GetDepartmentByNameAsync(
                department.Name
            );
            if (departmentGet is not null)
            {
                throw new Exception(
                    $"Echec de création d'un département : Il existe déjà un département avec ce nom {department.Name}"
                );
            }

            var departmentTocreate = new Department()
            {
                Name = department.Name,
                Description = department.Description,
                Address = department.Address,
            };

            var departmentCreated = await _departmentRepository.CreateDepartmentAsync(
                departmentTocreate
            );

            return new ReadDepartment()
            {
                Id = departmentCreated.DepartmentId,
                Name = departmentCreated.Name,
            };
        }

        public async Task<List<ReadDepartment>> GetDepartments()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();

            List<ReadDepartment> readDepartments = new List<ReadDepartment>();

            foreach (var department in departments)
            {
                readDepartments.Add(
                    new ReadDepartment()
                    {
                        Id = department.DepartmentId,
                        Name = department.Name,
                        Description = department.Description,
                        Address = department.Address,
                    }
                );
            }

            return readDepartments;
        }

        public async Task<ReadDepartment> GetDepartmentByIdAsync(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);

            if (department is null)
                throw new Exception(
                    $"Echec de recupération des informations d'un département car il n'existe pas : {departmentId}"
                );

            return new ReadDepartment() { Id = department.DepartmentId, Name = department.Name, };
        }

        public async Task<Department> UpdateDepartmentAsync(
            int departmentId,
            UpdateDepartment department
        )
        {
            var departmentUpdate =
                await _departmentRepository.GetDepartmentByIdAsync(departmentId)
                ?? throw new Exception(
                    $"Echec de mise à jour d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}"
                );

            var departmentGet = await _departmentRepository.GetDepartmentByNameAsync(
                department.Name
            );
            if (departmentGet is not null && departmentId != departmentGet.DepartmentId)
            {
                throw new Exception(
                    $"Echec de mise à jour d'un département : Il existe déjà un département avec ce nom {department.Name}"
                );
            }

            departmentUpdate.Name = department.Name;
            departmentUpdate.Description = department.Description;
            departmentUpdate.Address = department.Address;

            return await _departmentRepository.UpdateDepartmentAsync(departmentUpdate);
        }

        public async Task<Department> DeleteDepartmentById(int departmentId)
        {
            var departmentGet =
                await _departmentRepository.GetDepartmentByIdWithIncludeAsync(departmentId)
                ?? throw new Exception(
                    $"Echec de suppression d'un département : Il n'existe aucun departement avec cet identifiant : {departmentId}"
                );

            if (departmentGet.Employees.Any())
            {
                throw new Exception(
                    "Echec de suppression car ce departement est lié à des employés"
                );
            }

            return await _departmentRepository.DeleteDepartmentByIdAsync(departmentId);
        }
    }
}
