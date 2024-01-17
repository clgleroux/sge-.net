namespace backend.Dto.Attendance
{
    public class CreateAttendance
    {
        public int EmployeeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
