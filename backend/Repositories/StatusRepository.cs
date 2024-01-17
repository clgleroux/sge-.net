using backend.Dto.EmployeeDepartment;
using backend.Entities;
using backend.Infrastructures.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class StatusRepository
    {
        private readonly ManageEmployeeDbContext _manageEmployeeDbContext;

        public StatusRepository(ManageEmployeeDbContext manageEmployeeDbContext)
        {
            _manageEmployeeDbContext = manageEmployeeDbContext;
        }

        public async Task<List<Status>> GetStatusesAsync()
        {
            return await _manageEmployeeDbContext.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int statusId)
        {
            return await _manageEmployeeDbContext.Statuses.FirstOrDefaultAsync(
                x => x.StatusId == statusId
            );
        }

        

    }
}
