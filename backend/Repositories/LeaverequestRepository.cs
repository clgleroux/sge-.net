using backend.Dto.EmployeeDepartment;
using backend.Entities;
using backend.Infrastructures.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LeaverequestRepository
    {
        private readonly ManageEmployeeDbContext _manageEmployeeDbContext;

        public LeaverequestRepository(ManageEmployeeDbContext manageEmployeeDbContext)
        {
            _manageEmployeeDbContext = manageEmployeeDbContext;
        }

        public async Task<Leaverequest> CreateLeaverequestAsync(Leaverequest leaverequestToCreate)
        {
            await _manageEmployeeDbContext.Leaverequests.AddAsync(leaverequestToCreate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return leaverequestToCreate;
        }

        public async Task<List<Leaverequest>> GetLeaverequestsAsync()
        {
            return await _manageEmployeeDbContext.Leaverequests.ToListAsync();
        }

        public async Task<Leaverequest> GetLeaverequestByIdAsync(int leaverequestId)
        {
            return await _manageEmployeeDbContext.Leaverequests.FirstOrDefaultAsync(
                x => x.LeaveRequestId == leaverequestId
            );
        }

        public async Task<Leaverequest> GetLeaverequestByIdWithIncludeAsync(int leaverequestId)
        {
            return await _manageEmployeeDbContext
                .Leaverequests.Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.LeaveRequestId == leaverequestId);
        }

        public async Task<Leaverequest> UpdateLeaverequestAsync(Leaverequest leaverequestToUpdate)
        {
            _manageEmployeeDbContext.Leaverequests.Update(leaverequestToUpdate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return leaverequestToUpdate;
        }

        public async Task<Leaverequest> DeleteLeaverequestByIdAsync(int leaverequestId)
        {
            var leaverequestToDelete = await _manageEmployeeDbContext.Leaverequests.FindAsync(
                leaverequestId
            );
            _manageEmployeeDbContext.Leaverequests.Remove(leaverequestToDelete);
            await _manageEmployeeDbContext.SaveChangesAsync();
            return leaverequestToDelete;
        }
    }
}
