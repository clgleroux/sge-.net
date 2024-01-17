using backend.Dto.EmployeeDepartment;
using backend.Entities;
using backend.Infrastructures.Database;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class AttendanceRepository
    {
        private readonly ManageEmployeeDbContext _manageEmployeeDbContext;

        public AttendanceRepository(ManageEmployeeDbContext manageEmployeeDbContext)
        {
            _manageEmployeeDbContext = manageEmployeeDbContext;
        }

        public async Task<Attendance> CreateAttendanceAsync(Attendance attendanceToCreate)
        {
            await _manageEmployeeDbContext.Attendances.AddAsync(attendanceToCreate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return attendanceToCreate;
        }

        public async Task<List<Attendance>> GetAttendancesAsync()
        {
            return await _manageEmployeeDbContext.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetAttendanceByIdAsync(int attendanceId)
        {
            return await _manageEmployeeDbContext.Attendances.FirstOrDefaultAsync(
                x => x.AttendanceId == attendanceId
            );
        }

        public async Task<Attendance> GetAttendanceByIdWithIncludeAsync(int attendanceId)
        {
            return await _manageEmployeeDbContext
                .Attendances.Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.AttendanceId == attendanceId);
        }

        public async Task<Attendance> UpdateAttendanceAsync(Attendance attendanceToUpdate)
        {
            _manageEmployeeDbContext.Attendances.Update(attendanceToUpdate);
            await _manageEmployeeDbContext.SaveChangesAsync();

            return attendanceToUpdate;
        }

        public async Task<Attendance> DeleteAttendanceByIdAsync(int attendanceId)
        {
            var attendanceToDelete = await _manageEmployeeDbContext.Attendances.FindAsync(
                attendanceId
            );
            _manageEmployeeDbContext.Attendances.Remove(attendanceToDelete);
            await _manageEmployeeDbContext.SaveChangesAsync();
            return attendanceToDelete;
        }
    }
}
