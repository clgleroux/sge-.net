namespace backend.Dto.Attendance
{
    public class ReadAttendance
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
